import { Injectable } from '@angular/core';
import { ParameterBaseService } from 'shared/services/base-services/parameter-base.service';
import { ConfigurationService } from 'shared/services/application/configuration.service';
import { DataService } from 'shared/services/application/data.service';
import { Log } from 'shared/models';

@Injectable()
export class LogService extends ParameterBaseService<Log> {
	constructor(public _configurationService: ConfigurationService, public service: DataService) {
		super(_configurationService, service)
		this._serviceApi = '/api/Log/';
	}
}