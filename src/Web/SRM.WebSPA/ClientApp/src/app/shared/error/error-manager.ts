import { ErrorHandler, Injectable, Injector, Inject, isDevMode, NgZone } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { MessageService } from 'prime/message/messageservice';
import { AuthService } from 'shared/services';
import { NotificationService } from 'prime/message/notification.service';
@Injectable()
export class ErrorManager {
	constructor(
		private messageService: MessageService,
		private notificationService: NotificationService,
		private zone: NgZone

	) {
	}

	public handleError(errorResponse: any) {
		if (errorResponse !== "") {
			this.showMessage("İşlem sırasında hata alındı.");
			const message = errorResponse.message ? errorResponse.message : errorResponse.toString();
			this.showErrorInConsole(errorResponse);
		}
	}

	public handleHttpErrorResponse(errorResponse: HttpErrorResponse) {
		this.showErrorInConsole(errorResponse);
		if (isDevMode()) {
			this.showErrors(errorResponse); 
		}
		else {
			if ((errorResponse.error.state!=null && (errorResponse.error.state == 1 ||errorResponse.error.state == 2)) 
			|| (errorResponse.error.errors != null && errorResponse.error.errors.length > 0)) {
				this.showErrors(errorResponse);
			}
			else {
				if (errorResponse.error.exceptionId) {
					this.showMessage("İşlem sırasında hata alındı. Hata Id : " + errorResponse.error.exceptionId);
				}
				else {
					this.showMessage("İşlem sırasında hata alındı.");
				}
			}
		}
	}

	public showErrors(response: any) {
		if (response.error) {
			if (response.error.errors != null && response.error.errors.length > 0) {
				for (var i = 0; i < response.error.errors.length; i++) {
					var error = response.error.errors[i];
					this.showMessage(error);
				}
			}
			else if (response.error.error) {
				this.showMessage(response.error.error);
			} else if (typeof response === "string") {
				this.showMessage(response);
			}
			else {
				this.showMessage("Bilinmeyen bir hata oluştu.");
			}
		}
	}

	showMessage(message) {
		this.zone.runOutsideAngular(() => {
			var timeout = setTimeout(() => {
				this.zone.run(() => {
					this.messageService.add({ severity: 'error', summary: 'İşlem Sırasında Hata', detail: message });
				});
			}, 10);
		});
	}

	public showErrorInConsole(error: any): void {

		if (console && console.group && console.error) {
			console.group("Error Log");
			console.error(error);
			console.error(error.message);
			if (error.stack) {
				console.error(error.stack);
			}
			console.groupEnd();
		}
	}
}