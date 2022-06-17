import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';
import { ApplicationParameter, IBaseResponse } from 'shared/models';
import { ConfigurationService } from 'shared/services/application/configuration.service';
import { DataService } from 'shared/services/application/data.service';
import { ParameterBaseService } from 'shared/services/base-services/parameter-base.service';
@Injectable()
export class ApplicationParameterService extends ParameterBaseService<ApplicationParameter> {

	constructor(public _configurationService: ConfigurationService, public service: DataService) {
		super(_configurationService, service)
		this._serviceApi = '/api/ApplicationParameter/';
	}


	getByName(name, params?: any) {
		let url = this._apiUrl + this._serviceApi + "GetByName/" + name;
		return this.service.get<IBaseResponse>(url, params);
	}
}
