import { NgModule, ErrorHandler } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LocationStrategy, HashLocationStrategy } from '@angular/common';
import 'rxjs/add/operator/toPromise';

import * as examples from 'examples/demo';
import * as examplesApp from 'examples/app';
import * as examplesLayout from 'examples/layouts';
import { PrimeModule } from 'prime/prime.module';
import { ModuleWithProviders } from '@angular/core';

@NgModule({
    imports: [
        BrowserModule,
        RouterModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        BrowserAnimationsModule,  
        PrimeModule,
    ],
    declarations: [
        /* Examples Component */
        examplesLayout.ExamplesAppLayoutComponent,
        examplesApp.ExamplesAppMenuComponent,
        examplesApp.ExamplesAppSubMenuComponent,
        examplesApp.ExamplesAppTopBarComponent,
        examplesApp.ExamplesAppFooterComponent,
        examples.DashboardDemoComponent,
        examples.SampleDemoComponent,
        examples.FormsDemoComponent,
        examples.DataDemoComponent,
        examples.PanelsDemoComponent,
        examples.OverlaysDemoComponent,
        examples.MenusDemoComponent,
        examples.MessagesDemoComponent,
        examples.MessagesDemoComponent,
        examples.MiscDemoComponent,
        examples.ChartsDemoComponent,
        examples.EmptyDemoComponent,
        examples.FileDemoComponent,
        examples.UtilsDemoComponent,
        examples.DocumentationComponent,
        /* Examples Component */
    ],
    providers: [
        examples.CarService, examples.CountryService, examples.EventService, examples.NodeService
    ],
})
export class ExampleModule {
}
