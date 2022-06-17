import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';
import { StudentService } from 'shared/models';
import { ConfigurationService } from 'shared/services/application/configuration.service';
import { DataService } from 'shared/services/application/data.service';
import { ParameterBaseService } from 'shared/services/base-services/parameter-base.service';
@Injectable()
export class StudentServiceService extends ParameterBaseService<StudentService> {

	constructor(public _configurationService: ConfigurationService, public service: DataService) {
		super(_configurationService, service)
		this._serviceApi = '/api/StudentService/';
	}
}
