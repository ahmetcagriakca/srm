import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutes } from './app.routes';
import 'rxjs/add/operator/toPromise';

import { PrimeModule } from 'prime/prime.module';
import { ExampleModule } from 'examples/examples.module';
import { SharedModule } from 'shared/shared.module';
import { AppComponent } from 'application-srm/master';
import { IndividualsModule } from 'individuals/individuals.module';
import { SecurityModule } from 'security/security.module';
import { CoursesModule } from 'courses/courses.module';
import { ShuttlesModule } from 'shuttles/shuttles.module';
import { ApplicationSrmModule } from 'application-srm/application.module';
import { HelpModule } from './help/help.module';
import { ReportsModule } from 'reports/reports.module';
import { ApplicationManagementModule } from 'application-management/application-management.module';
@NgModule({
	imports: [
		BrowserModule,
		FormsModule,
		ReactiveFormsModule,
		AppRoutes,
		HttpClientModule,
		BrowserAnimationsModule,
		PrimeModule,
		ExampleModule,
		SharedModule.forRoot(),
		ApplicationSrmModule,
		IndividualsModule,
		CoursesModule,
		ReportsModule,
		SecurityModule,
		ShuttlesModule,
		ApplicationManagementModule,
		HelpModule,
	],
	bootstrap: [AppComponent]
})
export class AppModule { }
