import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';
import { ConfigurationService } from 'shared/services/application/configuration.service';
import { DataService } from 'shared/services/application/data.service';
import { StudentReport } from 'shared/models';
import { StudentBaseService } from 'shared/services/base-services/student-base.service';
@Injectable()
export class StudentReportService extends StudentBaseService<StudentReport> {

    constructor(public _configurationService: ConfigurationService, public service: DataService) {
        super(_configurationService, service)
        this._serviceApi = '/api/StudentReport/';
    }
}
