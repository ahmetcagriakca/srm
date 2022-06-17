import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import 'rxjs/add/operator/toPromise';

import * as masterComponents from 'application-srm/master';
import * as layoutComponents from 'application-srm/layouts';
import { PrimeModule } from 'prime/prime.module';
import { SharedModule } from 'shared/shared.module';
import { AppLayoutComponent } from 'application-srm/layouts';
import { HomeComponent } from 'application-srm/home';

@NgModule({
	imports: [
		BrowserModule,
		FormsModule,
		ReactiveFormsModule,
		HttpClientModule,
		BrowserAnimationsModule,
		PrimeModule,
		SharedModule.forRoot(),
	],
	declarations: [
		masterComponents.AppComponent,
		masterComponents.AppMenuComponent,
		masterComponents.AppSubMenuComponent,
		masterComponents.AppTopBarComponent,
		masterComponents.AppFooterComponent,
		layoutComponents.LoginLayoutComponent,
		layoutComponents.AppLayoutComponent,
		HomeComponent
	],
})
export class ApplicationSrmModule {
}
