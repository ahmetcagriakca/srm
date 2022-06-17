import { Component, OnInit, Injectable } from '@angular/core';
import {
	NgForm,
	FormGroup,
	FormControl,
	Validators
} from '@angular/forms';
import { MessageService } from 'prime/message/messageservice';
import { ParameterBasePageComponent } from 'shared/components';
import { Role, User } from 'shared/models';
import { RoleService, UserService } from 'shared/services';
import { ConfirmationService } from 'primeng/primeng';


@Component({
	selector: 'role',
	templateUrl: './role.component.html'
})

export class RoleComponent extends ParameterBasePageComponent<Role, RoleService> {
	selectedUser: User[];
	selectedRoles: Role[];
	Roles: Role[];
	constructor(public service: RoleService,
		public messageService: MessageService,
		public userService: UserService,
		public confirmationService: ConfirmationService,
	) {
		super(Role, service, messageService, confirmationService);
	}

	ngOnInit() {
		this.baseForm = new FormGroup({
			'id': new FormControl(''),
			'name': new FormControl('', Validators.required),
			'description': new FormControl('', Validators.required),
			'isActive': new FormControl(''),
		});
		this.searchForm = new FormGroup({
			'id': new FormControl(''),
			'name': new FormControl('', Validators.required),
			'description': new FormControl('', Validators.required),
			'isActive': new FormControl(''),
		});
		this.searchForm.controls['isActive'].setValue(null);
		this.baseForm.controls['isActive'].setValue(true);
		super.ngOnInit();
	}
}