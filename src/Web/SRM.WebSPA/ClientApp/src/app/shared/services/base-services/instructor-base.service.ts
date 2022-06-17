import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';
import { IBaseModel, IBaseResponse } from 'shared/models';
import { BaseService } from 'shared/services/base-services/base.service';
import { ConfigurationService } from 'shared/services/application/configuration.service';
import { DataService } from 'shared/services/application/data.service';
@Injectable()
export abstract class InstructorBaseService<T extends IBaseModel> extends BaseService {

    constructor(public _configurationService: ConfigurationService, public service: DataService) {
        super(_configurationService)
    }

    get(instructorId, params?): any {
        let url = this._apiUrl + this._serviceApi + instructorId;//searchDate=
        return this.service.get<IBaseResponse>(url, params);
    }

    getById(instructorId, id, params?): any {
        let url = this._apiUrl + this._serviceApi + instructorId + "/" +  id;//searchDate=
        return this.service.get<IBaseResponse>(url, params);
    }

    post(instructorId, data, params?) {
        let url = this._apiUrl + this._serviceApi + instructorId;
        return this.service.post<IBaseResponse>(url, data, params);
    }

    put(instructorId, id, data, params?): any {
        let url = this._apiUrl + this._serviceApi + instructorId + "/" +  id;
        return this.service.put<IBaseResponse>(url, data, params);
    }

    delete(instructorId, id, params?): any {
        let url = this._apiUrl + this._serviceApi + instructorId + "/" +  id;
        return this.service.delete<IBaseResponse>(url, params);
    }
}
