import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';
import { ConfigurationService } from 'shared/services/application/configuration.service';
import { DataService } from 'shared/services/application/data.service';
import { IBaseResponse } from 'shared/models';
import { BaseService } from 'shared/services/base-services/base.service';
import { Subject, Observable } from 'rxjs';
@Injectable()
export class AccountService extends BaseService {
	constructor(public _configurationService: ConfigurationService, private service: DataService) {
		super(_configurationService)
		this._serviceApi = '/api/Account/';
	}

	getDrivers(params?: any) {
		let url = this._apiUrl + this._serviceApi + "GetDrivers";
		return this.service.get<IBaseResponse>(url, params);
	}

	getRoles(params?: any) {
		let url = this._apiUrl + this._serviceApi + "GetRoles";
		return this.service.get<IBaseResponse>(url, params);
	}
	
	changePassword(data, params?: any) {
		let url = this._apiUrl + this._serviceApi + "changePassword";
		return this.service.post<IBaseResponse>(url, data, params);
	}

}
