import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';
import { ConfigurationService } from 'shared/services/application/configuration.service';
import { DataService } from 'shared/services/application/data.service';
import { IBaseResponse } from 'shared/models';
import { BaseService } from 'shared/services/base-services/base.service';
import { Subject } from 'rxjs';
@Injectable()
export class StudentCallService extends BaseService {

	constructor(public _configurationService: ConfigurationService, private service: DataService) {
		super(_configurationService)
		this._serviceApi = '/api/StudentCall/';
	}

	getShuttleOperationStudentLocations(params?: any) {
		let url = this._apiUrl + this._serviceApi + "GetShuttleOperationStudentLocations";
		return this.service.get<IBaseResponse>(url, params);
	}

	saveStudentCall(data, params?: any): any {
		let url = this._apiUrl + this._serviceApi + "SaveStudentCall";
		return this.service.post<IBaseResponse>(url, data, params);
	}

}
