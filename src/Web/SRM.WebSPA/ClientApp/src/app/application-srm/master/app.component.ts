import { Component } from '@angular/core';
import { Subscription } from 'rxjs';
import { AuthService, ConfigurationService } from 'shared/services';

@Component({
	selector: 'app-root',
	templateUrl: './app.component.html',
	styleUrls: ['./app.component.css']
})
export class AppComponent {
	subscription: Subscription;
	constructor(private securityService: AuthService, private configurationService: ConfigurationService) {
	}

	ngOnInit() {

		//Get configuration from server environment variables:
		console.log('configuration');
		this.configurationService.load();
		this.subscription = this.configurationService.settingsLoaded$.subscribe(res => {
			if (this.securityService.CanRefresh()) {
				var user = this.securityService.GetUser()
				this.securityService.getUserData()
					.subscribe(response => {
					});
			}
		});
	}

}
