import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';
import { IBaseModel, IBaseResponse } from 'shared/models';
import { BaseService } from 'shared/services/base-services/base.service';
import { ConfigurationService } from 'shared/services/application/configuration.service';
import { DataService } from 'shared/services/application/data.service';
@Injectable()
export abstract class ParameterBaseService<T extends IBaseModel> extends BaseService {

	constructor(public _configurationService: ConfigurationService, public service: DataService) {
		super(_configurationService)
	}


	getById(id: number, params?: any) {
		let url = this._apiUrl + this._serviceApi + id;
		return this.service.getById<IBaseResponse>(url, params);
	}

	search(params?: any) {
		let url = this._apiUrl + this._serviceApi + "search";
		return this.service.get<IBaseResponse>(url, params);
	}

	get(params?: any) {
		let url = this._apiUrl + this._serviceApi;
		return this.service.get<IBaseResponse>(url, params);
	}
	post(data, params?: any) {
		let url = this._apiUrl + this._serviceApi;
		return this.service.post<IBaseResponse>(url, data, params);
	}

	put(id, data, params?: any): any {
		let url = this._apiUrl + this._serviceApi + id;
		return this.service.put<IBaseResponse>(url, data, params);
	}

	delete(id, params?: any): any {
		let url = this._apiUrl + this._serviceApi + id;
		return this.service.delete<IBaseResponse>(url, params);
	}
}
