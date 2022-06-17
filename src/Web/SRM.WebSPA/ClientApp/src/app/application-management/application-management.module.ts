import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import 'rxjs/add/operator/toPromise';

import * as applicationManagementComponents from 'application-management/index';
import { PrimeModule } from 'prime/prime.module';
import { SharedModule } from 'shared/shared.module';

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
    exports: [
        applicationManagementComponents.LogComponent,
        applicationManagementComponents.PageComponent,
        applicationManagementComponents.ErrorComponent,
        applicationManagementComponents.UnauthorizedComponent,
    ],
    declarations: [
        applicationManagementComponents.LogComponent,
        applicationManagementComponents.PageComponent,
        applicationManagementComponents.ErrorComponent,
        applicationManagementComponents.UnauthorizedComponent,
    ],
})
export class ApplicationManagementModule {
}
