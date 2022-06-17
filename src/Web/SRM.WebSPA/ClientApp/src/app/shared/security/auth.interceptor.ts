import { throwError as observableThrowError, Observable, BehaviorSubject } from 'rxjs';

import { catchError, switchMap, finalize } from 'rxjs/operators';
import { Injectable, Injector, isDevMode } from "@angular/core";
import { HttpInterceptor, HttpRequest, HttpHandler, HttpSentEvent, HttpHeaderResponse, HttpProgressEvent, HttpResponse, HttpUserEvent, HttpErrorResponse, HttpEvent, HttpParams } from "@angular/common/http";

import { AuthService } from 'shared/services';


@Injectable()
export class AuthInterceptor implements HttpInterceptor {
	constructor(private inj: Injector) { }
	intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpSentEvent | HttpHeaderResponse | HttpProgressEvent | HttpResponse<any> | HttpUserEvent<any>> {
		return Observable.create((observer: any) => {
			setTimeout(() => {
				const authService = this.inj.get(AuthService)
				observer.next(authService.GetToken())
				observer.complete()
			})
		})
			.mergeMap((Authorization: string) => {
				let authReq = req

				let cleanedParams = new HttpParams();
				if (req.params.keys()) {
					req.params.keys().forEach(x => {
						if (req.params.get(x) != undefined)
							cleanedParams = cleanedParams.append(x, req.params.get(x));
					})
				}
				if (Authorization) {
					authReq = req.clone({
						setHeaders: {
							Authorization
						},
						params: cleanedParams
					});
				}
				else {
					authReq = req.clone({
						params: cleanedParams
					});
				}
				return next.handle(authReq)
			}).catch(error => {
				if (error instanceof HttpErrorResponse) {
					switch ((<HttpErrorResponse>error).status) {
						case 400:
							return this.handle400Error(error);
						case 401:
							return this.handle401Error(req, next);
					}
				} else {
					return Observable.throw(error);
				}
			});;

	}

	isRefreshingToken: boolean = false;
	tokenSubject: BehaviorSubject<string> = new BehaviorSubject<string>(null);

	handle401Error(req: HttpRequest<any>, next: HttpHandler) {
		const authService = this.inj.get(AuthService)
		if (!this.isRefreshingToken) {
			this.isRefreshingToken = true;
			if (!authService.CanRefresh()) {
				authService.logoutUser();
				return;
			}
			this.tokenSubject.next(null);
			return authService.Refresh()
				.pipe(
					switchMap((newToken: string) => {
						if (newToken) {
							this.tokenSubject.next(newToken);
							return Observable.create((observer: any) => {
								setTimeout(() => {
									const authService = this.inj.get(AuthService)
									observer.next(authService.GetToken())
									observer.complete()
								})
							})
								.mergeMap((Authorization: string) => {
									let authReq = req

									let cleanedParams = new HttpParams();
									if (req.params.keys()) {
										req.params.keys().forEach(x => {
											if (req.params.get(x) != undefined)
												cleanedParams = cleanedParams.append(x, req.params.get(x));
										})
									}
									if (Authorization) {
										authReq = req.clone({
											setHeaders: {
												Authorization
											},
											params: cleanedParams
										});
									}
									else {
										authReq = req.clone({
											params: cleanedParams
										});
									}
									return next.handle(authReq)
								});
						}
						return authService.logoutUser();
					}),
					catchError(error => {
						this.isRefreshingToken = false;
						if (error instanceof HttpErrorResponse) {
							switch ((<HttpErrorResponse>error).status) {
								case 400:
									return this.handle400Error(error);
								case 401:
									return authService.logoutUser();
							}
						} else {
							return Observable.throw(error);
						}
					}),
					finalize(() => {
						this.isRefreshingToken = false;
					})
				);

		} else {
			return this.tokenSubject
				.filter(token => token != null)
				.take(1)
				.switchMap(token => {
					return Observable.create((observer: any) => {
						setTimeout(() => {
							const authService = this.inj.get(AuthService)
							observer.next(authService.GetToken())
							observer.complete()
						})
					})
						.mergeMap((Authorization: string) => {
							let authReq = req

							let cleanedParams = new HttpParams();
							if (req.params.keys()) {
								req.params.keys().forEach(x => {
									if (req.params.get(x) != undefined)
										cleanedParams = cleanedParams.append(x, req.params.get(x));
								})
							}
							if (Authorization) {
								authReq = req.clone({
									setHeaders: {
										Authorization
									},
									params: cleanedParams
								});
							}
							else {
								authReq = req.clone({
									params: cleanedParams
								});
							}
							return next.handle(authReq)
						});
				});
		}
	}

	handle400Error(error) {
		if (error && error.status === 400 && error.error && !error.error.isSuccess && error.error.resultValue && error.error.resultValue.error === 'invalid_grant') {
			// If we get a 400 and the error message is 'invalid_grant', the token is no longer valid so logout.
			const authService = this.inj.get(AuthService)
			return authService.logoutUser();
		}

		return observableThrowError(error);
	}
}