import { OnInit } from '@angular/core';
import {
	NgForm,
	FormGroup,
} from '@angular/forms';
import { MessageService } from 'prime/message/messageservice';
import { IBaseModel } from 'shared/models';
import { BasePageComponent } from 'shared/components/basepages/basepage.component';
import { StudentComponent } from 'individuals/student-management';
import { StudentBaseService } from 'shared/services/base-services/student-base.service';
import { StudentService } from 'shared/services';
import { ConfirmationService } from 'primeng/primeng';
export abstract class StudentBasePageComponent<T extends IBaseModel, ST extends StudentBaseService<T>> extends BasePageComponent {

	public rowData: T[];
	public displayDialog: boolean;
	public newRecord: boolean;
	public record: T;
	public records: T[];
	public deleteId: any;
	public fullname: string;
	searchForm: FormGroup;
	studentId: number;
	constructor(
		public getType: new () => T,
		public studentService: StudentService,
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

		this.studentService.studentLoaded$.subscribe(studentId => {
			this.studentId = Number(studentId);
			this.loadData(this.studentId);
		});
		this.studentService.studentCleaned$.subscribe(studentId => {
			super.cleanForm();
			this.rowData = [];
			this.record = this.getNew();
		});
	}

	initData(rowData: T[]) {
		// return "Init Data not implemented";
		return rowData;
	}

	loadData(studentId) {
		this.service.get(studentId)
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
				this.service.put(this.studentId, this.record.id, this.baseForm.value)
					.subscribe(response => {
						if (response.isSuccess) {
							this.messageService.add({ severity: 'success', summary: 'İşlem Başarılı', detail: 'Kayıt Başarıyla Güncellendi.' });
							this.loadData(this.studentId);
							this.displayDialog = false;
						} else {
							this.showErrors(response);
						}
					});
			} else {
				this.service.post(this.studentId, this.baseForm.value)
					.subscribe(response => {
						if (response.isSuccess) {
							this.messageService.add({ severity: 'success', summary: 'İşlem Başarılı', detail: 'Kayıt Başarıyla Eklendi.' });
							this.loadData(this.studentId);
							this.displayDialog = false;
						} else {
							this.showErrors(response);
						}
					});
			}
		}
		else {
			this.messageService.add({ severity: 'warn', summary: 'Uyarı', detail: 'Formu doğru şekilde doldurunuz.' });
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
			confirmationMessage = this.deleteId + " idli kaydı silmek istediğinize emin misiniz?";
		}
		this.confirmationService.confirm({
			message: confirmationMessage,
			header: 'Onaylıyor musunuz?',
			icon: 'pi pi-question-circle',
			accept: () => {
				this.service.delete(this.studentId, this.deleteId)
					.subscribe(response => {
						if (response.isSuccess) {
							this.messageService.add({ severity: 'success', summary: 'İşlem Başarılı', detail: 'Kayıt Başarıyla Silindi' });
							this.deleteId = 0;
							this.loadData(this.studentId,
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