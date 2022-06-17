import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';
import { ConfigurationService } from 'shared/services/application/configuration.service';
import { DataService } from 'shared/services/application/data.service';
import { IBaseResponse } from 'shared/models';
import { BaseService } from 'shared/services/base-services/base.service';
import { Subject } from 'rxjs';
@Injectable()
export class ShuttleService extends BaseService {

	public shuttleListLoadedSource = new Subject();
	shuttleListLoad$ = this.shuttleListLoadedSource.asObservable();

	public studentCallLoadedSource = new Subject();
	studentCallLoad$ = this.studentCallLoadedSource.asObservable();

	public studentAdviceCallLoadedSource = new Subject();
	studentAdviceCallLoad$ = this.studentAdviceCallLoadedSource.asObservable();

	public shuttleAdviceByIdLoadSource = new Subject();
	public shuttleAdviceByIdLoad$ = this.shuttleAdviceByIdLoadSource.asObservable();

	public studentCallCompleteSource = new Subject();
	studentCallComplete$ = this.studentCallCompleteSource.asObservable();

	public studentAvailableTimeShowSource = new Subject();
	studentAvailableTimeShow$ = this.studentAvailableTimeShowSource.asObservable();

	public studentAdviceFinishSource = new Subject();
	studentAdviceFinish$ = this.studentAdviceFinishSource.asObservable();
	
	public shuttleStudentStatusUpdateSource = new Subject();
	shuttleStudentStatusUpdate$ = this.shuttleStudentStatusUpdateSource.asObservable();

	public shuttleOperationLocationSource = new Subject();
	shuttleOperationLocationLoad$ = this.shuttleOperationLocationSource.asObservable();

	constructor(public _configurationService: ConfigurationService, private service: DataService) {
		super(_configurationService)
		this._serviceApi = '/api/Shuttle/';
	}

	getStudentShuttleCallList(params?: any) {
		let url = this._apiUrl + this._serviceApi + "GetStudentShuttleCallList";
		return this.service.get<IBaseResponse>(url, params);
	}

	getShuttleOperationById(params?: any) {
		let url = this._apiUrl + this._serviceApi + "GetShuttleOperationById";
		return this.service.get<IBaseResponse>(url, params);
	}

	getStudentShuttleOperationById(params?: any) {
		let url = this._apiUrl + this._serviceApi + "GetStudentShuttleOperationById";
		return this.service.get<IBaseResponse>(url, params);
	}

	getStudentOperationListByDate(params?: any) {
		let url = this._apiUrl + this._serviceApi + "GetStudentOperationListByDate";
		return this.service.get<IBaseResponse>(url, params);
	}

	getShuttleOperationStudentLocations(params?: any) {
		let url = this._apiUrl + this._serviceApi + "GetShuttleOperationStudentLocations";
		return this.service.get<IBaseResponse>(url, params);
	}



	getStudentOperationAdvicesByShuttleOperationId(id: number, params?: any) {
		let url = this._apiUrl + this._serviceApi + "GetStudentOperationAdvicesByShuttleOperationId/" + id;
		return this.service.getById<IBaseResponse>(url, params);
	}

	getStudentShuttleDailyCallListByStudent(params?: any) {
		let url = this._apiUrl + this._serviceApi + "GetStudentShuttleDailyCallListByStudent";
		return this.service.getById<IBaseResponse>(url, params);
	}

	getStudentShuttleAdviceById(id: number, params?: any) {
		let url = this._apiUrl + this._serviceApi + "GetStudentShuttleAdviceById" + "/" + id;
		return this.service.getById<IBaseResponse>(url, params);
	}

	setAdviceToOperation(data, params?: any): any {
		let url = this._apiUrl + this._serviceApi + "SetAdviceToOperation" ;
		return this.service.put<IBaseResponse>(url, data, params);
	}

	setStudentShuttleOperationStatus(data, params?: any): any {
		let url = this._apiUrl + this._serviceApi + "SetStudentShuttleOperationStatus";
		return this.service.put<IBaseResponse>(url, data, params);
	}


	createDailyAdvice(data, params?: any) {
		let url = this._apiUrl + this._serviceApi + "CreateAdvice";
		return this.service.post<IBaseResponse>(url, data, params);
	}


	setShuttleOperationStatus(data, params?: any): any {
		let url = this._apiUrl + this._serviceApi + "SetShuttleOperationStatus";
		return this.service.put<IBaseResponse>(url, data, params);
	}
	createCustomStudentOperation(data, params?: any) {
		let url = this._apiUrl + this._serviceApi + "CreateCustomStudentOperation";
		return this.service.post<IBaseResponse>(url, data, params);
	}

	setStudentOperastionLessonsCount(data, params?: any): any {
		let url = this._apiUrl + this._serviceApi + "SetStudentOperastionLessonsCount";
		return this.service.put<IBaseResponse>(url, data, params);
	}
}
