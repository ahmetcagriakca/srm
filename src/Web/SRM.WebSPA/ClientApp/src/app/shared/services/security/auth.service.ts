import { Injectable } from '@angular/core';
import { Response } from '@angular/http';
import 'rxjs/add/operator/map';
import { Observable } from 'rxjs/Rx';
import { Subject } from 'rxjs/Subject';
import { Router, RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { ConfigurationService } from 'shared/services/application/configuration.service';
import { StorageService } from 'shared/services/application/storage.service';
import { IToken } from 'shared/models';
import { LoaderService } from 'shared/components/loader/loader.service';
import { delay, map, catchError } from 'rxjs/operators';
@Injectable()
export class AuthService {

	getApplicationValue(name): any {
		return this.storage.retrieve(name);
	}
	setApplicationValue(name, value): any {
		this.storage.store(name, value);
	}
	_serviceApi: string = '/connect/token/';
	private actionUrl: string;
	private headers: HttpHeaders;
	private identityUrl = '';

	private authenticationSource = new Subject<boolean>();
	authenticationChallenge$ = this.authenticationSource.asObservable();
	private userDataSource = new Subject<boolean>();
	userDataSourceLoader$ = this.userDataSource.asObservable();
	constructor(
		private _http: HttpClient,
		private _router: Router,
		private _configurationService: ConfigurationService,
		private storage: StorageService,
		private loaderService: LoaderService) {

		if (this._configurationService.isReady) { this.identityUrl = this._configurationService.serverSettings.identityUrl; }
		else {
			this._configurationService.settingsLoaded$.subscribe(x => {
				this.identityUrl = this._configurationService.serverSettings.identityUrl
			});
		}

		if (this.getApplicationValue('IsAuthorized') !== '') {
			this.IsAuthorized = (this.getApplicationValue('IsAuthorized'));
			this.UserData = this.getApplicationValue('userData');
			this.authenticationSource.next(true);
		}
		if (!this.Authenticated()) {
			this.ResetAuthorizationData();
		}
	}

	public IsAuthorized: boolean = false;

	public GetToken(): any {
		if (this.TokenNotExpired()) {
			return this.getApplicationValue('authorizationData');
		}
		else {
			return null;
		}
	}
	public TokenNotExpired() {
		return this.getApplicationValue('authorizationData') != null && this.getApplicationValue('authorizationData') != "";
	}
	public Authenticated() {
		return this.TokenNotExpired() && this.IsAuthorized;
	}

	public ResetAuthorizationData() {
		this.setApplicationValue('authorizationData', '');
		this.setApplicationValue('authorizationDataRefreshToken', '');
		this.setApplicationValue('authorizationDataIdToken', '');
		this.setApplicationValue('userData', '');
		this.IsAuthorized = false;
		this.setApplicationValue('IsAuthorized', false);
		this.authenticationSource.next(false);
	}
	public CanRefresh() {
		var token = this.getApplicationValue('authorizationData');
		var refreshToken = this.getApplicationValue('authorizationDataRefreshToken');
		return token !== '' && refreshToken !== '';
	}

	private state: RouterStateSnapshot;
	public logoutUser(url?) {
		if (!url) {
			url = this._router.routerState.snapshot.url;
		}
		if (url.indexOf("login?returnUrl") < 0) {
			this.ResetAuthorizationData();
			this._router.navigate(['/login'], { queryParams: { returnUrl: url } });
			return Observable.throw("Tekrar giriş yapmanız gerekiyor.");
		}
	}

	public Refresh() {
		var token = this.getApplicationValue('authorizationData');
		var refreshToken = this.getApplicationValue('authorizationDataRefreshToken');
		var refreshRequest = {
			token: token,
			refreshToken: refreshToken,
		};
		// this.ResetAuthorizationData();
		let authorizationUrl = this.identityUrl + this._serviceApi + 'Refresh';
		return this._http.post(authorizationUrl, refreshRequest, { headers: this.headers })
			.map((response: any) => {
				if (response.resultValue.token) {
					this.SetAuthorizationData(response.resultValue.token, response.resultValue.refreshToken);
					return response.resultValue.token;
				}
				else {
					return response.resultValue.token;
				}
			})

	}

	public Authorize(user: IToken) {
		this.showLoader();
		this.ResetAuthorizationData();
		let authorizationUrl = this.identityUrl + this._serviceApi;
		debugger;
		return this._http.post(authorizationUrl, user, { headers: this.headers })

			.map((response: any) => {
				if (response.access_token) {
					this.SetAuthorizationData(response.access_token, null);
					return true;
				}
				else {
					return false;
				}
			}).pipe()
			.finally(() => {
				this.onEnd();
			});
	}

	public SetAuthorizationData(token: any, refreshToken: any) {
		if (this.getApplicationValue('authorizationData') !== '') {
			this.setApplicationValue('authorizationData', '');
		}

		this.setApplicationValue('authorizationData', token);
		this.setApplicationValue('authorizationDataRefreshToken', refreshToken);
		this.IsAuthorized = true;
		this.setApplicationValue('IsAuthorized', true);

		this.getUserData()
			.subscribe(response => {
				this.authenticationSource.next(true);
				// emit observable
			});
	}
	private UserData: any;


	public getUserData = (): any => {
		if (this.identityUrl === '')
			this.identityUrl = this.getApplicationValue('identityUrl');

		return this._http.get(this.identityUrl + this._serviceApi + 'getUserData',
			{
				headers: this.headers
			})

			.finally(() => {
				this.onEnd();

			})
			.map((response: any) => {
				this.SetUserData(response.resultValue);
				this.userDataSource.next();
				return response;
			})
	}

	public GetUser = (): any => {
		if (this.UserData != null) {
			return this.UserData;
		}
		if (this.getApplicationValue('userData') !== '') {
			this.UserData = this.getApplicationValue('userData');
		}
		return this.UserData;
	}

	public SetUserData(response) {
		this.UserData = response;
		this.setApplicationValue('userData', response);
	}

	public SignOut() {
		this.showLoader();
		this._http.get(this.identityUrl + this._serviceApi + 'SignOut', {
			headers: this.headers
		})
			.map(response => <any>(response))
			.finally(() => {
				this.onEnd();
				this.ResetAuthorizationData();
			})
			.subscribe(response => {
				if (response.isSuccess) {
					this._router.navigate(['/login']);/*, { queryParams: { returnUrl: state.url } }*/
				}
			});
	}

	getUserPages() {
		var url = this.identityUrl + this._serviceApi + "GetUserPages";
		return this._http.get(url)
			.map(response => <any>(<Response>response));
	}

	checkUserPagePermission(path: string) {
		var url = this.identityUrl + this._serviceApi + "CheckUserPagePermission/?path=" + (path ? path : "home");
		return this._http.get(url)
			.map(response => <any>(<Response>response));
	}

	private setHeaders() {
		this.headers = new HttpHeaders();
		this.headers.append('Content-Type', 'application/json');
		this.headers.append('Accept', 'application/json');
		this.headers.append('Access-Control-Allow-Origin', '*');

		let token = this.GetToken();

		if (token !== '') {
			this.headers.append('Authorization', 'Bearer ' + token);
		}
	}

	public ShowError(error: any) {
		console.log(error);
	}


	private onEnd(): void {
		this.hideLoader();
	}

	private showLoader(): void {
		this.loaderService.show();
	}

	private hideLoader(): void {
		this.loaderService.hide();
	}

}
