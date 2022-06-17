import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';
import { ConfigurationService } from 'shared/services/application/configuration.service';
import { DataService } from 'shared/services/application/data.service';
import { IBaseResponse } from 'shared/models';
import { BaseService } from 'shared/services/base-services/base.service';
@Injectable()
export class DashboardService extends BaseService {

	constructor(public _configurationService: ConfigurationService, private service: DataService) {
		super(_configurationService)
		this._serviceApi = '/api/Dashboard/';
	}

	get() {
		let url = this._apiUrl + this._serviceApi;
		return this.service.get<IBaseResponse>(url);
	}

	getDailyLessonStatusStatistics(params?: any) {
		let url = this._apiUrl + this._serviceApi + "GetDailyLessonStatusStatistics";
		return this.service.getById<IBaseResponse>(url, params);
	}

	getComingStatusStatistics(params?: any) {
		let url = this._apiUrl + this._serviceApi + "GetComingStatusStatistics";
		return this.service.getById<IBaseResponse>(url, params);
	}
}
