import { OnInit } from '@angular/core';
import {
	NgForm,
	FormGroup,
} from '@angular/forms';
import { MessageService } from 'prime/message/messageservice';
import { IBaseModel } from 'shared/models';
import { BasePageComponent } from 'shared/components/basepages/basepage.component';
import { InstructorService } from 'shared/services';
import { ConfirmationService } from 'primeng/primeng';
import { InstructorBaseService } from 'shared/services/base-services/instructor-base.service';
export abstract class InstructorBasePageComponent<T extends IBaseModel, ST extends InstructorBaseService<T>> extends BasePageComponent {

	public rowData: T[];
	public displayDialog: boolean;
	public newRecord: boolean;
	public record: T;
	public records: T[];
	public deleteId: any;
	public fullname: string;
	searchForm: FormGroup;
	instructorId: number;
	constructor(
		public getType: new () => T,
		public instructorService: InstructorService,
		public service: ST,
		public messageService = new MessageService,
		public confirmationService: ConfirmationService,
	) {
		super(messageService)
		this.record = this.getNew();
	}

	getNew(): T {
		return new this.getType();
	}

	ngOnInit() {
		this.deleteId = 0;

		this.instructorService.instructorLoaded$.subscribe(instructorId => {
			this.instructorId = Number(instructorId);
			this.loadData(this.instructorId);
		});
		this.instructorService.instructorCleaned$.subscribe(instructorId => {
			super.cleanForm();
			this.rowData = [];
			this.record = this.getNew();
		});
	}

	initData(rowData: T[]) {
		// return "Init Data not implemented";
		return rowData;
	}

	loadData(instructorId) {
		this.service.get(instructorId)
			.subscribe(response => {
				this.rowData = this.initData(<T[]>response.resultValue);
			});
	}

	initAdd(record: T) {
		// return "Init Data not implemented";
	}

	showDialogToAdd() {
		this.newRecord = true;
		this.deleteId = 0;
		this.record = this.getNew();
		this.initAdd(this.record);
		this.displayDialog = true;
	}

	initEdit(record: T) {
		// return "Init Edit not implemented";
	}

	showDialogToEdit(record: T) {
		this.newRecord = false;
		this.record = Object.assign({}, record);
		this.initEdit(this.record);
		this.displayDialog = true;
	}
	beforeSave(record: T) {
	}
	save() {
		if (super.isValid()) {
			this.beforeSave(this.baseForm.value);
			if (this.newRecord == false) {
				this.service.put(this.instructorId, this.record.id, this.baseForm.value)
					.subscribe(response => {
						if (response.isSuccess) {
							this.messageService.add({ severity: 'success', summary: '????lem Ba??ar??l??', detail: 'Kay??t Ba??ar??yla G??ncellendi.' });
							this.loadData(this.instructorId);
							this.displayDialog = false;
						} else {
							this.showErrors(response);
						}
					});
			} else {
				this.service.post(this.instructorId, this.baseForm.value)
					.subscribe(response => {
						if (response.isSuccess) {
							this.messageService.add({ severity: 'success', summary: '????lem Ba??ar??l??', detail: 'Kay??t Ba??ar??yla Eklendi.' });
							this.loadData(this.instructorId);
							this.displayDialog = false;
						} else {
							this.showErrors(response);
						}
					});
			}
		}
		else {
			this.messageService.add({ severity: 'warn', summary: 'Uyar??', detail: 'Formu do??ru ??ekilde doldurunuz.' });
		}
	}

	cancel() {
		this.record = this.getNew();
		this.displayDialog = false;
	}

	showDialogToDelete(record: T, message?: any) {
		this.deleteId = record.id;
		var confirmationMessage = "";
		if (message) {
			confirmationMessage = message;
		} else {
			confirmationMessage = this.deleteId + " idli kayd?? silmek istedi??inize emin misiniz?";
		}
		this.confirmationService.confirm({
			message: confirmationMessage,
			header: 'Onayl??yor musunuz?',
			icon: 'pi pi-question-circle',
			accept: () => {
				this.service.delete(this.instructorId, this.deleteId)
					.subscribe(response => {
						if (response.isSuccess) {
							this.messageService.add({ severity: 'success', summary: '????lem Ba??ar??l??', detail: 'Kay??t Ba??ar??yla Silindi' });
							this.deleteId = 0;
							this.loadData(this.instructorId,
							);
						} else {
							this.showErrors(response);
						}
					});
			},
			reject: () => {
			}
		});
		this.displayDialog = false;
	}

	register(baseForm: NgForm) {
		this.save();
	}

	isValid(baseForm: any) {
		return baseForm.valid;
	}
}