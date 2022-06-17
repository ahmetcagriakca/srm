import { Injectable } from '@angular/core';
import { ConfigurationService } from 'shared/services/application/configuration.service';
@Injectable()
export abstract class BaseService {
    _apiUrl: string;
    _serviceApi: string;
    _methodName: string;
    constructor(public _configurationService: ConfigurationService) {
        if (this._configurationService.isReady) { 
			this._apiUrl = this._configurationService.serverSettings.applicationUrl; 
		}
        else {
            this._configurationService.settingsLoaded$.subscribe(x => {
                this._apiUrl = this._configurationService.serverSettings.applicationUrl
            });
        }
    }

}
