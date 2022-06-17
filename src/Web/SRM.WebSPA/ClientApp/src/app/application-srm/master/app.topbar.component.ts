import { Component } from '@angular/core';
import { AuthService, ConfigurationService } from 'shared/services';
import { AppLayoutComponent } from 'application-srm/layouts';
import { IUser, IRole } from 'shared/models';
import { Subscription } from 'rxjs';

@Component({
	selector: 'app-topbar',
	templateUrl: './app.topbar.component.html'
})
export class AppTopBarComponent {
	user: IUser;
	role: IRole;
	subscription: Subscription;
	constructor(public app: AppLayoutComponent, public securityService: AuthService, public _configurationService : ConfigurationService) { }

	ngOnInit() {
		var currentUser = this.securityService.GetUser();
		if (currentUser) {
			this.user = currentUser.user;
			if (currentUser.roles && currentUser.roles.length > 0) {
				this.role = currentUser.roles[0];
			}
		}
		
		this.subscription = this.securityService.userDataSourceLoader$.subscribe(res => {
			var currentUser = this.securityService.GetUser();
			if (currentUser) {
				this.user = currentUser.user;
				if (currentUser.roles && currentUser.roles.length > 0) {
					this.role = currentUser.roles[0];
				}
			}
		});
	}


	logout() {
		this.securityService.SignOut();
	}
}
