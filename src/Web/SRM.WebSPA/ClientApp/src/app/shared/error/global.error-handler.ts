import { ErrorHandler, Injectable, Injector, Inject } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { AuthService } from 'shared/services';
import { ErrorManager } from 'shared/error/error-manager';
import { LoaderService } from 'shared/components/loader/loader.service';
@Injectable()
export class GlobalErrorHandler implements ErrorHandler {
	constructor(private injector: Injector,
		@Inject(ErrorManager) private errorManager: ErrorManager,
		@Inject(LoaderService) private loaderService: LoaderService
	) {
	}

	public get auth(): AuthService {
		return this.injector.get(AuthService);
	}

	handleError(errorResponse: any): void {
		this.loaderService.hide();
		if (errorResponse.status == 403) {
		}
		else if (errorResponse.status == 401) {
			this.auth.logoutUser();
		}
		else {
			if (errorResponse instanceof HttpErrorResponse) {

				this.errorManager.handleHttpErrorResponse(errorResponse);
			}
			else {
				this.errorManager.handleError(errorResponse);
			}
		}
	}
}