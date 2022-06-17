import { Component, OnInit, Injectable } from '@angular/core';
import {
	NgForm,
	FormGroup,
	FormControl,
	Validators
} from '@angular/forms';
import { MessageService } from 'prime/message/messageservice';
import { ParameterBasePageComponent } from 'shared/components';
import { User, Role } from 'shared/models';
import { UserService, RoleService } from 'shared/services';
import { ConfirmationService } from 'primeng/primeng';


@Component({
	selector: 'user',
	templateUrl: './user.component.html'
})

export class UserComponent extends ParameterBasePageComponent<User, UserService> {
	selectedUser: User[];
	selectedRoles: Role[];
	Roles: Role[];
	constructor(public service: UserService,
		public messageService: MessageService,
		public userService: UserService,
		public confirmationService: ConfirmationService,
		public roleService: RoleService,
	) {
		super(User, service, messageService, confirmationService);
	}

	ngOnInit() {

		this.baseForm = new FormGroup({
			'id': new FormControl(''),
			'vzUserId': new FormControl(''),
			'userName': new FormControl('', Validators.required),
			'firstName': new FormControl('', Validators.required),
			'lastName': new FormControl('', Validators.required),
			'email': new FormControl('', [
				Validators.required,
				Validators.pattern("[^ @]*@[^ @]*")
			]),
			'userRoles': new FormControl('', Validators.required),
			'isActive': new FormControl(''),
			'isDeleted': new FormControl(''),
		});

		this.searchForm = new FormGroup({
			'id': new FormControl(''),
			'vzUserId': new FormControl(''),
			'userName': new FormControl(''),
			'firstName': new FormControl(''),
			'lastName': new FormControl(''),
			'email': new FormControl('', [
				Validators.pattern("[^ @]*@[^ @]*")
			]),
			'userRoles': new FormControl(''),
			'isActive': new FormControl(''),
			'isDeleted': new FormControl(''),
		});
		this.roleService.get()
			.subscribe(response => {
				this.Roles = (<Role[]>response.resultValue);
			});
		this.service.get()
			.subscribe(response => {
				this.rowData = (<User[]>response.resultValue);
			});
		super.ngOnInit();
	}

	onRowSelect(event) {
		this.selectedRoles = event.data.userRoles;
	}
}