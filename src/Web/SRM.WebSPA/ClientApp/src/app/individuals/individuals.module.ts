import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import 'rxjs/add/operator/toPromise';

import * as studentComponents from 'individuals/student-management';
import * as instructorComponents from 'individuals/instructor-management';
import * as parameterComponents from 'individuals/parameters';
import { PrimeModule } from 'prime/prime.module';
import { SharedModule } from 'shared/shared.module';
import { ObstacleTypeComponent, HospitalComponent } from 'individuals/parameters';

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
	exports:[
        studentComponents.StudentAvailableTimeComponent,
        studentComponents.StudentAddressComponent,
        studentComponents.StudentContactComponent
	],
    declarations: [
        studentComponents.StudentSearchComponent,
        studentComponents.StudentEvolutionComponent,
        studentComponents.StudentReportComponent,
        studentComponents.StudentInstructorRelationComponent,
        studentComponents.StudentAddressComponent,
        studentComponents.StudentAvailableTimeComponent,
        studentComponents.StudentComponent,
        instructorComponents.InstructorSearchComponent,
        instructorComponents.InstructorStudentRelationComponent,
        instructorComponents.InstructorComponent,
        parameterComponents.ObstacleTypeComponent,
        parameterComponents.HospitalComponent,
        studentComponents.StudentContactComponent
    ],
})
export class IndividualsModule {
}
