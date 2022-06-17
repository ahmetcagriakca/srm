import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';
import { ConfigurationService } from 'shared/services/application/configuration.service';
import { DataService } from 'shared/services/application/data.service';
import { IBaseResponse } from 'shared/models';
import { BaseService } from 'shared/services/base-services/base.service';
import { Subject } from 'rxjs';
@Injectable()
export class InstructorService extends BaseService {

	public instructorLoadedSource = new Subject();
	public instructorLoaded$ = this.instructorLoadedSource.asObservable();
	public instructorCleanedSource = new Subject();
	public instructorCleaned$ = this.instructorCleanedSource.asObservable();
	public isInstructorClean: boolean;

	private isInstructorCleanSource = new Subject<boolean>();

	isInstructorCleanObserver = this.isInstructorCleanSource.asObservable();

	changeClean(isClean: boolean) {
		this.isInstructorClean = isClean;
	}

    constructor(public _configurationService: ConfigurationService, private service: DataService) {
        super(_configurationService)
        this._serviceApi = '/api/Instructor/';
    }

    get() {
        let url = this._apiUrl + this._serviceApi;
        return this.service.get<IBaseResponse>(url);
    }

    getById(id: number) {
        let url = this._apiUrl + this._serviceApi +  id;
        return this.service.getById<IBaseResponse>(url);
    }

    getByIdentityNumber(identityNumber: string) {
        let url = this._apiUrl + this._serviceApi + "GetByIdentityNumber" + "/" + identityNumber;
        return this.service.getById<IBaseResponse>(url);
    }

    search(params?: any) {
        let url = this._apiUrl + this._serviceApi + "search";
        return this.service.get<IBaseResponse>(url, params);
    }

    post(data) {
        let url = this._apiUrl + this._serviceApi;
        return this.service.post<IBaseResponse>(url, data);
    }

    put(id, data): any {
        let url = this._apiUrl + this._serviceApi + id;
        return this.service.put<IBaseResponse>(url, data);
    }


}
