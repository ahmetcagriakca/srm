import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import 'rxjs/add/operator/toPromise';

import { PrimeModule } from 'prime/prime.module';
import { SharedModule } from 'shared/shared.module';
import { BranchComponent } from 'courses/parameters';

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
        BranchComponent,
    ],
})
export class CoursesModule {
}
