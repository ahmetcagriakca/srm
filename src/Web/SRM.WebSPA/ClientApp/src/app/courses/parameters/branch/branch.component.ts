import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MessageService } from 'prime/message/messageservice';
import { ParameterBasePageComponent } from 'shared/components';
import { Branch } from 'shared/models';
import { BranchService } from 'shared/services';
import { ConfirmationService } from 'primeng/primeng';

@Component({
	selector: 'branch',
	templateUrl: './branch.component.html'
})

export class BranchComponent extends ParameterBasePageComponent<Branch, BranchService>  {
	constructor(
		public service: BranchService,
		public messageService: MessageService,
		public confirmationService: ConfirmationService
	) {
		super(Branch, service, messageService, confirmationService);
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
