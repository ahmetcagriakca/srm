import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { MessageService } from 'prime/message/messageservice';
import { Message, Messages } from 'primeng/primeng';
import { AuthService, ConfigurationService } from 'shared/services';
import { IToken } from 'shared/models';
import { LoaderService } from 'shared/components/loader/loader.service';


@Component({
	selector: 'login',
	templateUrl: './login.component.html',
	styles: [`
		.app-name {
			display: inline-block;
			font-size: 28px;
			vertical-align: middle;
			color: #04838f;
			margin-left: 8px;
			font-weight: bold;
		}
 `]
})

export class LoginComponent implements OnInit {
	returnUrl: string;
	baseForm: FormGroup;
	msgs: Message[];
	constructor(
		private route: ActivatedRoute,
		private router: Router,
		private auth: AuthService,
		private messageService: MessageService,
		private loaderService: LoaderService,
		public _configurationService: ConfigurationService
	) {

	}

	ngOnInit() {
		this.loaderService.hide();
		this.baseForm = new FormGroup({
			'userName': new FormControl('', Validators.required),
			'password': new FormControl('', Validators.required),
			'rememberMe': new FormControl(''),
		});
		this.baseForm.patchValue({
			'rememberMe': true,
			// formControlName2: myValue2 (can be omitted)
		});
		// get return url from route parameters or default to '/'
		this.auth.ResetAuthorizationData();
	}

	login() {
		let userToken: IToken = this.baseForm.value;
		this.auth.Authorize(userToken)
			.subscribe(response => {
				var result = response.valueOf;
				if (this.auth.Authenticated()) {
					//todo:get user data with promise or pipe
					this.auth.getUserData()
						.subscribe(response => {
							if (response.resultValue) {
								this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
								this.router.navigate([this.returnUrl]);
								//window.location.href = window.location.origin + window.location.pathname + "#" + this.returnUrl;
								// this.auth.SetUserData(response.resultValue);
							}
							// emit observable
						});
					return;
				}
				else {
					window.location.href = window.location.origin + window.location.pathname + "Home/UnAuthorized";
					return;
				}
			},
				error => {
					if (error.status == 401 || (error.status == 400 && error.error.state == 2)) {
						this.messageService.clear();
						this.messageService.add({ severity: 'error', detail: 'Kulllanıcı adı ya da şifre hatalı' });
					}
					else {
						throw error;
					}
				});
	}
}
