import { Injectable } from '@angular/core';
import { ParameterBaseService } from 'shared/services/base-services/parameter-base.service';
import { ConfigurationService } from 'shared/services/application/configuration.service';
import { DataService } from 'shared/services/application/data.service';
import { Role } from 'shared/models';

@Injectable()
export class RoleService extends ParameterBaseService<Role> {

	constructor(public _configurationService: ConfigurationService, public service: DataService) {
		super(_configurationService, service)
		this._serviceApi = '/api/Role/';
	}
}