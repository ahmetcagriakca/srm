import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';
import { Hospital, LocationRegion,IBaseResponse } from 'shared/models';
import { ConfigurationService } from 'shared/services/application/configuration.service';
import { DataService } from 'shared/services/application/data.service';
import { ParameterBaseService } from 'shared/services/base-services/parameter-base.service';
import { BaseService } from 'shared/services';
@Injectable()
export class LocationRegionService extends ParameterBaseService<LocationRegion> {

    constructor(public _configurationService: ConfigurationService, public service: DataService) {
        super(_configurationService, service)
        this._serviceApi = '/api/LocationRegion/';
    }

    getActiveRegions(params?: any) {
		let url = this._apiUrl + this._serviceApi + "GetActiveRegions" ;
		return this.service.get<IBaseResponse>(url, params);
	}
}
