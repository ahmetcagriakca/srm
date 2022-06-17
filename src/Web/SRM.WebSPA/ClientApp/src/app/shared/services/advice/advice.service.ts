import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';
import { ConfigurationService } from 'shared/services/application/configuration.service';
import { DataService } from 'shared/services/application/data.service';
import { IBaseResponse } from 'shared/models';
import { BaseService } from 'shared/services/base-services/base.service';
import { Subject } from 'rxjs';
@Injectable()
export class AdviceService extends BaseService {

	public adviceListForMapLoadedSource = new Subject();
    adviceListForMapLoad$ = this.adviceListForMapLoadedSource.asObservable();
    
	constructor(public _configurationService: ConfigurationService, private service: DataService) {
		super(_configurationService)
		this._serviceApi = '/api/Advice/';
	}

	getAdviceForMap(data, params?: any) {
		let url = this._apiUrl + this._serviceApi + "GetAdvices";
		return this.service.post<IBaseResponse>(url, data, params);
	}
}
