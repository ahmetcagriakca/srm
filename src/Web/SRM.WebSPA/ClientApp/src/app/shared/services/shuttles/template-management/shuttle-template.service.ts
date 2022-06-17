import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';
import { ConfigurationService } from 'shared/services/application/configuration.service';
import { DataService } from 'shared/services/application/data.service';
import { IBaseResponse } from 'shared/models';
import { BaseService } from 'shared/services/base-services/base.service';
import { Subject } from 'rxjs';
@Injectable()
export class ShuttleTemplateService extends BaseService {
	constructor(public _configurationService: ConfigurationService, private service: DataService) {
		super(_configurationService)
		this._serviceApi = '/api/ShuttleTemplate/';
	}

	getShuttleTemplateByDayOfWeek(dayOfWeek?: number, params?: any) {
		let url = this._apiUrl + this._serviceApi + "GetShuttleTemplateByDayOfWeek" + (dayOfWeek === undefined ? "/0" : ("/" + dayOfWeek));
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

	CreateStudentTemplate(data, params?: any): any {
		let url = this._apiUrl + this._serviceApi + "CreateStudentTemplate";
		return this.service.post<IBaseResponse>(url, data, params);
	}

	UpdateStudentTemplate(data, params?: any): any {
		let url = this._apiUrl + this._serviceApi + "UpdateStudentTemplate";
		return this.service.put<IBaseResponse>(url, data, params);
	}

	DeleteStudentTemplate(id, params?: any): any {
		let url = this._apiUrl + this._serviceApi + "DeleteStudentTemplate/" + id;
		return this.service.delete<IBaseResponse>(url, params);
	}

	DeleteShuttleTemplate(id, params?: any): any {
		let url = this._apiUrl + this._serviceApi + id;
		return this.service.delete<IBaseResponse>(url, params);
	}
}
