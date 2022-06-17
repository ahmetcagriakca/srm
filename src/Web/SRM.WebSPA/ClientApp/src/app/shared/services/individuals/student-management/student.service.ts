import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';
import { ConfigurationService } from 'shared/services/application/configuration.service';
import { DataService } from 'shared/services/application/data.service';
import { IBaseResponse } from 'shared/models';
import { BaseService } from 'shared/services/base-services/base.service';
import { Subject, Observable } from 'rxjs';
@Injectable()
export class StudentService extends BaseService {

	public studentLoadedSource = new Subject();
	public studentLoaded$ = this.studentLoadedSource.asObservable();
	public studentCleanedSource = new Subject();
	public studentCleaned$ = this.studentCleanedSource.asObservable();
	public isStudentClean: boolean;

	private isStudentCleanSource = new Subject<boolean>();

	isStudentCleanObserver = this.isStudentCleanSource.asObservable();

	changeClean(isClean: boolean) {
		this.isStudentClean = isClean;
	}

	constructor(public _configurationService: ConfigurationService, private service: DataService) {
		super(_configurationService)
		this._serviceApi = '/api/Student/';
	}

	getStudents() {
		let url = this._apiUrl + this._serviceApi ;
		return this.service.getById<IBaseResponse>(url);
	}

	getStudentById(id: number) {
		let url = this._apiUrl + this._serviceApi + "GetStudentById" + "/" + id;
		return this.service.getById<IBaseResponse>(url);
	}

	getStudentByLocationId(locationId: number) {
		let url = this._apiUrl + this._serviceApi + "GetStudentByLocationId" + "/" + locationId;
		return this.service.getById<IBaseResponse>(url);
	}

	GetStudentByIdentityNumber(identityNumber: string) {
		let url = this._apiUrl + this._serviceApi + "GetStudentByIdentityNumber" + "/" + identityNumber;
		return this.service.getById<IBaseResponse>(url);
	}

	searchStudents(params?: any) {
		let url = this._apiUrl + this._serviceApi + "search";
		return this.service.get<IBaseResponse>(url, params);
	}
	createStudent(data) {
		let url = this._apiUrl + this._serviceApi + "CreateStudent";
		return this.service.post<IBaseResponse>(url, data);
	}

	updateStudent(id, data): any {
		let url = this._apiUrl + this._serviceApi + "UpdateStudent" + "/" + id;
		return this.service.put<IBaseResponse>(url, data);
	}

	getStudentLessons(id, params?): any {
		let url = this._apiUrl + this._serviceApi + "GetStudentLessons" + "/" + id;
		return this.service.get<IBaseResponse>(url, params);
	}

	getStudentInstructorRelations(id, params?): any {
		let url = this._apiUrl + this._serviceApi + "GetStudentInstructorRelations" + "/" + id;
		return this.service.get<IBaseResponse>(url, params);
	}
}
