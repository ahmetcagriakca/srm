import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import 'rxjs/add/operator/toPromise';

import { PrimeModule } from 'prime/prime.module';
import { SharedModule } from 'shared/shared.module';
import { StudentParticipationStatusReport } from '.';

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
        StudentParticipationStatusReport,
    ],
})
export class ReportsModule {
}
