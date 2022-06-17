import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MessageService } from 'prime/message/messageservice';
import { ParameterBasePageComponent } from 'shared/components';
import { Hospital } from 'shared/models';
import { HospitalService } from 'shared/services';
import { ConfirmationService } from 'primeng/primeng';

@Component({
    selector: 'hospital',
    templateUrl: './hospital.component.html'
})

export class HospitalComponent extends ParameterBasePageComponent<Hospital, HospitalService> {
    constructor(
		public service: HospitalService, 
		public messageService: MessageService,
		public confirmationService: ConfirmationService,
	) {
        super(Hospital, service, messageService,confirmationService);
    }

    ngOnInit() {
        this.baseForm = new FormGroup({
            'id': new FormControl(''),
            'name': new FormControl('', Validators.required),
            'isActive': new FormControl('', Validators.required),
        });
        this.searchForm = new FormGroup({
            'id': new FormControl(''),
            'name': new FormControl(''),
            'isActive': new FormControl(null),
        });
        super.ngOnInit();
    }
}
