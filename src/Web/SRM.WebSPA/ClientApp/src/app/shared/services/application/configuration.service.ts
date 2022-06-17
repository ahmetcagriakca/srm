import { Injectable } from '@angular/core';

import 'rxjs/Rx';
import 'rxjs/add/observable/throw';
import 'rxjs/add/operator/map';
import { HttpClient } from '@angular/common/http';
import { Subject } from 'rxjs';
import { IConfiguration } from 'shared/models';
import { StorageService } from 'shared/services/application/storage.service';


@Injectable()
export class ConfigurationService {
	serverSettings: IConfiguration = {
		identityUrl: "",
		applicationUrl: "",
		applicationName: "",
	};
	// observable that is fired when settings are loaded from server
	public isReady: boolean = false;

	private settingsLoadedSource = new Subject();
	settingsLoaded$ = this.settingsLoadedSource.asObservable();
	constructor(private http: HttpClient, private storageService: StorageService) { }

	load() {
		if (this.storageService.retrieve('applicationUrl') !== ''  && this.storageService.retrieve('applicationUrl') !== undefined) {
			this.isReady = true;
			let serverConf: IConfiguration = {identityUrl: this.storageService.retrieve('identityUrl'),applicationUrl: this.storageService.retrieve('applicationUrl'), applicationName: this.storageService.retrieve('applicationName') };
			this.serverSettings = serverConf;
			this.settingsLoadedSource.next();
			this.getConfigurations();
		}
		else {
			this.getConfigurations();
		}
	}

	private getConfigurations() {
		const baseURI = document.baseURI.endsWith('/') ? document.baseURI : `${document.baseURI}/`;
		let url = `${baseURI}Home/Configuration`;
		this.http.get(url).subscribe((response: IConfiguration) => {
			console.log('server settings loaded');
			this.serverSettings = response;
			console.log(this.serverSettings);
			debugger;
			this.storageService.store('identityUrl', this.serverSettings.identityUrl);
			this.storageService.store('applicationUrl', this.serverSettings.applicationUrl);
			this.storageService.store('applicationName', this.serverSettings.applicationName);
			this.isReady = true;
			let serverConf: IConfiguration = { identityUrl: this.storageService.retrieve('identityUrl'),applicationUrl: this.storageService.retrieve('applicationUrl'), applicationName: this.storageService.retrieve('applicationName') };
			this.serverSettings = serverConf;
			this.settingsLoadedSource.next();
		});
	}
}
