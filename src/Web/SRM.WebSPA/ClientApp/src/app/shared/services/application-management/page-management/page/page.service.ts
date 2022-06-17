import { Injectable } from '@angular/core';
import { ParameterBaseService } from 'shared/services/base-services/parameter-base.service';
import { ConfigurationService } from 'shared/services/application/configuration.service';
import { DataService } from 'shared/services/application/data.service';
import { Page, IBaseResponse } from 'shared/models';

@Injectable()
export class PageService extends ParameterBaseService<Page> {

	constructor(public _configurationService: ConfigurationService, public service: DataService) {
		super(_configurationService, service)
		this._serviceApi = '/api/Page/';
	}

	getParents() {
		let url = this._apiUrl + this._serviceApi + "GetParents";
		return this.service.get<IBaseResponse>(url);
	}
}