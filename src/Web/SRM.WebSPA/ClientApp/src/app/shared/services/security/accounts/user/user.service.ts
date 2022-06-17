import { Injectable } from '@angular/core';
import { ParameterBaseService } from 'shared/services/base-services/parameter-base.service';
import { ConfigurationService } from 'shared/services/application/configuration.service';
import { DataService } from 'shared/services/application/data.service';
import { IBaseResponse } from 'shared/models/response/base-response.interface';
import { Observable } from 'rxjs';
import { User } from 'shared/models';

@Injectable()
export class UserService extends ParameterBaseService<User> {

	constructor(public _configurationService: ConfigurationService, public service: DataService) {
		super(_configurationService, service)
		this._serviceApi = '/api/User/';
	}
	get(id?: number, params?: any): Observable<any> {
		if (id) {
			let url = this._apiUrl + this._serviceApi + "GetById/" + id;
			return this.service.get<IBaseResponse>(url, params);
		} else {
			let url = this._apiUrl + this._serviceApi + "GetById";
			return this.service.get<IBaseResponse>(url, params);
		}
	}
}