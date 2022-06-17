import { NgModule, ErrorHandler, ModuleWithProviders } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LocationStrategy, HashLocationStrategy } from '@angular/common';
import 'rxjs/add/operator/toPromise';


import * as sharedComponents from 'shared/components';
import * as sharedServices from 'shared/services';
import { GlobalErrorHandler } from 'shared/error';
import { AuthInterceptor, AuthGuard } from 'shared/security';
import { PrimeModule } from 'prime/prime.module';

import 'rxjs/add/operator/toPromise';
import { BreadcrumbComponent } from 'shared/components/breadcrumb/breadcrumb.component';
import { ValidationTooltipModule } from 'shared/components/validators/validation-tooltip/validation-tooltip';
import { LoaderService } from 'shared/components/loader/loader.service';
import { LoaderComponent } from 'shared/components/loader/loader.component';
import { ErrorManager } from 'shared/error/error-manager';
@NgModule({
	imports: [
		BrowserModule,
		RouterModule,
		FormsModule,
		ReactiveFormsModule,
		HttpClientModule,
		BrowserAnimationsModule,
		PrimeModule,
		ValidationTooltipModule
	],
	exports: [
		sharedComponents.OnlyNumber,
		sharedComponents.AnchorHorizontalDirective,
		sharedComponents.SrmFormControl,
		ValidationTooltipModule,
		LoaderComponent,
		BreadcrumbComponent
	],
	declarations: [
		BreadcrumbComponent,
		sharedComponents.OnlyNumber,
		sharedComponents.AnchorHorizontalDirective,
		sharedComponents.SrmFormControl,
		LoaderComponent

	]
})
export class SharedModule {
	static forRoot(): ModuleWithProviders {
		return {
			ngModule: SharedModule,
			providers: [
				/* Services */
				AuthGuard,
				sharedServices.AuthService,
				sharedServices.ConfigurationService,
				sharedServices.StorageService,
				sharedServices.LoggerService,
				sharedServices.DataService,
				{ provide: LocationStrategy, useClass: HashLocationStrategy },
				{ provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
				{ provide: ErrorHandler, useClass: GlobalErrorHandler },
				sharedServices.StudentService,
				sharedServices.StudentReportService,
				sharedServices.LessonService,
				sharedServices.ObstacleTypeService,
				sharedServices.HospitalService,
				sharedServices.BranchService,
				sharedServices.InstructorService,
				sharedServices.InstructorStudentRelationService,
				sharedServices.StudentInstructorRelationService,
				sharedServices.StudentAddressService,
				sharedServices.StudentAvailableTimeService,
				sharedServices.LocationRegionService,
				sharedServices.StudentServiceService,
				sharedServices.ShuttleService,
				sharedServices.ShuttleTemplateService,
				sharedServices.ApplicationParameterService,
				sharedServices.ExcelService,
				sharedServices.AccountService,
				sharedServices.DashboardService,
				sharedServices.ReportService,
				LoaderService,
				ErrorManager,
				sharedServices.LogService,
				sharedServices.PageService,
				sharedServices.UserService,
				sharedServices.RoleService,
				sharedServices.IconService,
				sharedServices.StudentContactService,
				sharedServices.AdviceService,
				sharedServices.StudentCallService,
			],
		};
	}
}
