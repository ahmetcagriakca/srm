import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MessageService } from 'prime/message/messageservice';
import { ParameterBasePageComponent } from 'shared/components';
import { ObstacleType } from 'shared/models';
import { ObstacleTypeService } from 'shared/services';
import { ConfirmationService } from 'primeng/primeng';

@Component({
	selector: 'obstacle-type',
	templateUrl: './obstacle-type.component.html'
})

export class ObstacleTypeComponent extends ParameterBasePageComponent<ObstacleType, ObstacleTypeService>  {
	constructor(
		public service: ObstacleTypeService,
		public messageService: MessageService,
		public confirmationService: ConfirmationService,
	) {
		super(ObstacleType, service, messageService, confirmationService);
	}

	ngOnInit() {
		this.baseForm = new FormGroup({
			'id': new FormControl(''),
			'name': new FormControl('', Validators.required),
			'description': new FormControl('', Validators.required),
			'isActive': new FormControl('', Validators.required),
		});
		this.searchForm = new FormGroup({
			'id': new FormControl(''),
			'name': new FormControl(''),
			'description': new FormControl(''),
			'isActive': new FormControl(null),
		});
		super.ngOnInit();
	}
}
