import { Component, OnInit, Injectable } from '@angular/core';
import {
	NgForm,
	FormGroup,
	FormControl,
	Validators
} from '@angular/forms';
import { ParameterBasePageComponent } from 'shared/components';
import { Log } from 'shared/models';
import { LogService, UserService } from 'shared/services';
import { ConfirmationService, SelectItem } from 'primeng/primeng';
import { MessageService } from 'prime/message/messageservice';

@Component({
	selector: 'log',
	templateUrl: './logview.component.html'
})

export class LogComponent extends ParameterBasePageComponent<Log, LogService> {
	selectedLog: Log;
	levels: SelectItem[];
	constructor(public service: LogService,
		public messageService: MessageService,
		public userService: UserService,
		public confirmationService: ConfirmationService,
	) {
		super(Log, service, messageService, confirmationService);

	}

	ngOnInit() {

		this.baseForm = new FormGroup({
			'message': new FormControl('')
		});
		this.searchForm = new FormGroup({
			'message': new FormControl('')
		});

		this.levels = [];
		this.levels.push({ label: 'ALL', value: null });
		this.levels.push({ label: 'DEBUG', value: '2' });
		this.levels.push({ label: 'INFO', value: '3' });
		this.levels.push({ label: 'WARN', value: '4' });
		this.levels.push({ label: 'ERROR', value: '5' });
		this.levels.push({ label: 'FATAL', value: '6' });
		this.levels.push({ label: 'OFF', value: '7' });

		super.ngOnInit();
	}

	initData(rowData: Log[]) {
		if (this.rowData && this.rowData.length > 0) {
			for (var i = 0; i < this.rowData.length; i++) {
				var item = this.rowData[i];
				if (item.createdOn) {
					item.createdOn = new Date(item.createdOn);
				}
			}
		}
		return rowData
	}

	showDetails(item: Log) {
		this.newRecord = false;
		this.selectedLog = item;
		this.displayDialog = true;
	}
}