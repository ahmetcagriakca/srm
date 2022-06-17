import { OnInit } from '@angular/core';
import {
	NgForm,
	FormGroup,
} from '@angular/forms';
import { MessageService } from 'prime/message/messageservice';
import { IBaseModel } from 'shared/models';
import { ParameterBaseService } from 'shared/services';
import { BasePageComponent } from 'shared/components/basepages/basepage.component';
import { ConfirmationService } from 'primeng/primeng';
export abstract class ParameterBasePageComponent<T extends IBaseModel, ST extends ParameterBaseService<T>> extends BasePageComponent {

	public rowData: T[];
	public displayDialog: boolean;
	public displayDeleteDialog: boolean;
	public newRecord: boolean;
	public record: T;
	public records: T[];
	public deleteId: any;
	public fullname: string;
	searchForm: FormGroup;
	constructor(
		public getType: new () => T,
		public service: ST,
		public messageService = new MessageService,
		public confirmationService: ConfirmationService
	) {
		super(messageService)
		this.record = this.getNew();

	}

	getNew(): T {
		return new this.getType();
	}

	ngOnInit() {
		this.deleteId = 0;
		this.loadData();
	}

	loadData() {
		this.search(this.searchForm.value)
	}


	initData(rowData: T[]) {
		// return "Init Data not implemented";
		return rowData;
	}

	beforeSearch(searchFormData: any) {
		// return "Init Data not implemented";
		return searchFormData;
	}

	search(searchFormData?: any) {
		var searchFormValues = Object.assign({}, searchFormData);
		searchFormValues = this.beforeSearch(searchFormValues);
		this.service.search(searchFormValues)
			.subscribe(response => {
				if (response.isSuccess == true) {
					this.rowData = this.initData((<T[]>response.resultValue));
				}
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
			this.beforeSave(this.record);
			if (this.newRecord == false) {
				this.service.put(this.record.id, this.record)
					.subscribe(response => {
						if (response.isSuccess) {
							this.messageService.add({ severity: 'success', summary: 'İşlem Başarılı', detail: 'Kayıt Başarıyla Güncellendi.' });
							this.loadData();
							this.displayDialog = false;
						} else {
							this.showErrors(response);
						}
					});
			} else {
				this.service.post(this.record)
					.subscribe(response => {
						if (response.isSuccess) {
							this.messageService.add({ severity: 'success', summary: 'İşlem Başarılı', detail: 'Kayıt Başarıyla Eklendi.' });
							this.loadData();
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

	showDialogToDelete(record: T) {
		this.deleteId = record.id;
		this.confirmationService.confirm({
			message: this.deleteId + " idli kaydı silmek istediğinize emin misiniz?",
			header: 'Onaylıyor musunuz?',
			icon: 'pi pi-question-circle',
			accept: () => {
				this.service.delete(this.deleteId)
					.subscribe(response => {
						if (response.isSuccess) {

							this.messageService.add({ severity: 'success', summary: 'İşlem Başarılı', detail: 'Kayıt Başarıyla Silindi' });
							this.deleteId = 0;
							this.loadData();
							this.displayDialog = false;
						} else {
							this.showErrors(response);
						}
					});
			},
			reject: () => {
			}
		});
	}

	register(baseForm: NgForm) {
		this.save();
	}

	isValid(baseForm: any) {
		return baseForm.valid;
	}

	falseStateName: string = "Pasif";
	trueStateName: string = "Aktif";
	nullStateName: string = "Her ikisi de";
	triStatelabel: string = this.nullStateName;
	setStateLabel(state?: boolean) {
		switch (state) {
			case true: this.triStatelabel = this.trueStateName;
				break;
			case false: this.triStatelabel = this.falseStateName;
				break;
			case null: this.triStatelabel = this.nullStateName;
				break;
		}
	}

	onTriStateChange(event) {
		this.setStateLabel(event.value);
	}
}