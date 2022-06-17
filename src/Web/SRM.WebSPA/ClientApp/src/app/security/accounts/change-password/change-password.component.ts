import { Component, OnInit, Injectable } from '@angular/core';
import {
	NgForm,
	FormGroup,
	FormControl,
	Validators,
	FormBuilder
} from '@angular/forms';
import { MessageService } from 'prime/message/messageservice';
import { BasePageComponent } from 'shared/components';
import { Role, User } from 'shared/models';
import { RoleService, UserService, AuthService, AccountService } from 'shared/services';
import { ConfirmationService } from 'primeng/primeng';


@Component({
	selector: 'change-password.component',
	templateUrl: './change-password.component.html'
})


export class ChangePasswordComponent extends BasePageComponent {
	user: any;
	constructor(
		public messageService: MessageService,
		public confirmationService: ConfirmationService,
		public authService: AuthService,
		public accountService: AccountService,
		private fb: FormBuilder,
	) {
		super(messageService);
	}

	ngOnInit() {
		this.user = this.authService.GetUser().user;
		this.baseForm = this.fb.group({
			'oldPassword': new FormControl('', Validators.required),
			'passwords': this.fb.group({
				newPassword: ['', Validators.required],
				confirmNewPassword: ['', Validators.required]
			}, { validator: this.matchValidator })
		});
		super.ngOnInit();
	}
	matchValidator(group: FormGroup) {
		var valid = false;
		if (group.controls.newPassword.value == group.controls.confirmNewPassword.value) {
			valid = true;
		}
		if (valid) {
			return null;
		}

		return {
			mismatch: { message: "Şifre onayı, Yeni şifre ile aynı olmalıdır" },

		};
	}
	changePassword() {
		if (this.isValid()) {
			if (this.baseForm.value.newPassword != this.baseForm.value.confirmNewPassword) {
				var changePasswordRequest = {
					userId: this.user.userId,
					oldPassword: this.baseForm.value.oldPassword,
					newPassword: this.baseForm.value.newPassword,
				}
				this.accountService.changePassword(changePasswordRequest).subscribe(response => {

					if (response.isSuccess) {

						this.messageService.add({ severity: 'success', summary: 'İşlem Başarılı', detail: 'Şifre değişikliği gerçekleştirildi.' });
						this.cleanForm();
					} else {
						this.showErrors(response);
					}
				});
			}
			else {
				this.messageService.add({ severity: 'error', summary: 'İşlem Sırasında Hata', detail: 'Yeni şifre ile yeni şifre tekrarı aynı değil.' });
			}
		}
	}
}
