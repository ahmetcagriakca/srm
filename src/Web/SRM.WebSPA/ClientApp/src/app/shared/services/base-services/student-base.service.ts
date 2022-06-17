import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';
import { IBaseModel, IBaseResponse } from 'shared/models';
import { BaseService } from 'shared/services/base-services/base.service';
import { ConfigurationService } from 'shared/services/application/configuration.service';
import { DataService } from 'shared/services/application/data.service';
@Injectable()
export abstract class StudentBaseService<T extends IBaseModel> extends BaseService {

    constructor(public _configurationService: ConfigurationService, public service: DataService) {
        super(_configurationService)
    }

    get(studentId, params?): any {
        let url = this._apiUrl + this._serviceApi + studentId;//searchDate=
        return this.service.get<IBaseResponse>(url, params);
    }

    getById(studentId, id, params?): any {
        let url = this._apiUrl + this._serviceApi + studentId + "/" +  id;//searchDate=
        return this.service.get<IBaseResponse>(url, params);
    }

    post(studentId, data, params?) {
        let url = this._apiUrl + this._serviceApi + studentId;
        return this.service.post<IBaseResponse>(url, data, params);
    }

    put(studentId, id, data, params?): any {
        let url = this._apiUrl + this._serviceApi + studentId + "/" +  id;
        return this.service.put<IBaseResponse>(url, data, params);
    }

    delete(studentId, id, params?): any {
        let url = this._apiUrl + this._serviceApi + studentId + "/" +  id;
        return this.service.delete<IBaseResponse>(url, params);
    }
}
