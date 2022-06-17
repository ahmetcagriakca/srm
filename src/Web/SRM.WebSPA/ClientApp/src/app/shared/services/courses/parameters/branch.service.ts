import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';
import { ObstacleType, Branch } from 'shared/models';
import { ConfigurationService } from 'shared/services/application/configuration.service';
import { DataService } from 'shared/services/application/data.service';
import { ParameterBaseService } from 'shared/services/base-services/parameter-base.service';
@Injectable()
export class BranchService extends ParameterBaseService<Branch> {

    constructor(public _configurationService: ConfigurationService, public service: DataService) {
        super(_configurationService,service)
        this._serviceApi = '/api/Branch/';
    }
}
