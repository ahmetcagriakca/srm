import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import 'rxjs/add/operator/toPromise';

import { PrimeModule } from 'prime/prime.module';
import { SharedModule } from 'shared/shared.module';
import { LoginComponent } from 'security/login';
import { RoleComponent } from './accounts/role/role.component';
import { UserComponent } from './accounts/user/user.component';
import { ChangePasswordComponent } from './accounts/change-password/change-password.component';

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
		LoginComponent,
		RoleComponent,
		UserComponent,
		ChangePasswordComponent,
	],
})
export class SecurityModule {
}
