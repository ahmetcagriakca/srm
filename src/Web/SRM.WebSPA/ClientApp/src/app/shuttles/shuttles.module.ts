import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import 'rxjs/add/operator/toPromise';

import * as operationComponents from 'shuttles/operation-management';
import * as parameterComponents from 'shuttles/parameters';
import * as templateComponents from 'shuttles/template-management';
import { PrimeModule } from 'prime/prime.module';
import { SharedModule } from 'shared/shared.module';
import { IndividualsModule } from 'individuals/individuals.module';

@NgModule({
	imports: [
		BrowserModule,
		FormsModule,
		ReactiveFormsModule,
		HttpClientModule,
		BrowserAnimationsModule,
		PrimeModule,
		SharedModule.forRoot(),
		IndividualsModule,
	],
	exports:[
		operationComponents.ShuttleOperationLocationComponent,
	],
	declarations: [
		operationComponents.StudentCallComponent,
		operationComponents.StudentAdviceCallComponent,
		operationComponents.ShuttleStudentAdviceComponent,
		operationComponents.ShuttleStudentAdviceByIdComponent,
		operationComponents.ShuttleOperationLocationComponent,
		operationComponents.ShuttleListComponent,
		parameterComponents.LocationRegionComponent,
		parameterComponents.StudentServiceComponent,
		templateComponents.ShuttleTemplateListComponent,
	]
})
export class ShuttlesModule {
}
