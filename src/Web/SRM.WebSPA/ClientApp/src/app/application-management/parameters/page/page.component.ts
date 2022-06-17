import { Component, OnInit, Injectable } from '@angular/core';
import { NgForm, FormGroup, FormControl, Validators } from '@angular/forms';
import { ParameterBasePageComponent } from 'shared/components';
import { Page, Role } from 'shared/models';
import { PageService, RoleService, AccountService, IconService } from 'shared/services';
import { ConfirmationService, SelectItem } from 'primeng/primeng';
import { MessageService } from 'prime/message/messageservice';

@Component({
	selector: 'page',
	templateUrl: './page.component.html',
	styles: [`
        .icons-list {
            text-align: center;
        }

        .icons-list span {
            font-size: 2em;
        }

        .icons-list .ui-md-2 {
            padding-bottom: 2em;
        }
    `]
})

export class PageComponent extends ParameterBasePageComponent<Page, PageService> {
	Users: any;
	Parents: any[];
	Icons: any[];
	public Roles: SelectItem[];
	public Pages: SelectItem[];
	public selectedRoles: number[];
	public searchedParent: number;
	public searchedIcon: number;
	constructor(public service: PageService,
		public messageService: MessageService,
		public confirmationService: ConfirmationService,
		public accountService: AccountService,
		public iconService: IconService,
	) {
		super(Page, service, messageService, confirmationService);
	}

	ngOnInit() {
		this.baseForm = new FormGroup({
			'id': new FormControl(''),
			'name': new FormControl('', Validators.required),
			'url': new FormControl('', Validators.required),
			'componentName': new FormControl(''),
			'order': new FormControl('', Validators.required),
			'icon': new FormControl(''),
			'showOnMenu': new FormControl(true, Validators.required),
			'parent': new FormControl(''),
			'pageRoles': new FormControl('', Validators.required),
			'isActive': new FormControl(''),
		});
		this.searchForm = new FormGroup({
			'id': new FormControl(''),
			'name': new FormControl(''),
			'url': new FormControl(''),
			'componentName': new FormControl(''),
			'order': new FormControl(''),
			'icon': new FormControl(''),
			'showOnMenu': new FormControl(''),
			'parent': new FormControl(''),
			'pageRoles': new FormControl(''),
			'isActive': new FormControl(null),
		});
		this.searchForm.controls['showOnMenu'].setValue(null);
		this.searchForm.controls['isActive'].setValue(null);
		this.baseForm.controls['showOnMenu'].setValue(true);
		this.baseForm.controls['isActive'].setValue(true);
		this.Roles = [];
		this.accountService.getRoles()
			.subscribe(response => {
				var entities = (<Role[]>response.resultValue);
				for (let role of entities) {
					this.Roles.push({ label: role.description, value: role.id });
				}
			});


		this.service.get()
		this.getIcons();
		super.ngOnInit();
	}
	getIcons() {
		this.Icons = [{ label: "Seçiniz", value: null }];
		this.iconService.getIcons().subscribe((data: any) => {
			for (let icon of data) {
				this.Icons.push({ label: icon.name, value: icon.id });
			}
		});
	}

	beforeSearch(searchFormData: any) {
		if (!searchFormData.pageRoles) {
			delete searchFormData.pageRoles;
		}
		// return "Init Data not implemented";
		return searchFormData;
	}
	initData(rowData: Page[]) {
		this.Parents = [{ label: "Seçiniz", value: null }];
		this.service.getParents()
			.subscribe(response => {
				var entities = (<Page[]>response.resultValue);
				for (let page of entities) {
					this.Parents.push({ label: page.name, value: page.id });
				}
				if (this.searchedParent)
					this.searchForm.controls['parent'].setValue(this.searchedParent);
				if (this.searchedIcon)
					this.searchForm.controls['icon'].setValue(this.searchedIcon);
			});
		return rowData;
	}

	initAdd(record: Page) {
		this.selectedRoles = [];
		record.showOnMenu = true;
		record.isActive = true;
	}

	initEdit(record: Page) {
		this.selectedRoles = [];
		if (record.pageRoles && record.pageRoles.length > 0) {
			record.pageRoles.forEach(element => {
				this.selectedRoles.push(element.roleId);
			});
		}
		// var icon = this.iconService.getIconByName(record.icon);
		// record.icon = icon.id;
	}

	beforeSave(record: Page) {
		// var icon = this.iconService.getIcon(record.icon);
		// record.icon = icon.name;
		record.pageRoleIds = this.selectedRoles;
	}
}