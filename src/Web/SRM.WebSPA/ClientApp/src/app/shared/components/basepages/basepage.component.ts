import { OnInit } from '@angular/core';
import {
	NgForm,
	FormGroup,
} from '@angular/forms';
import { MessageService } from 'prime/message/messageservice';
import { BaseService } from 'shared/services';
import { IBaseModel, IBaseResponse } from 'shared/models';
import { LocaleSettings } from 'primeng/primeng';
export abstract class BasePageComponent implements OnInit {

	calender_locale_tr: LocaleSettings = {
		firstDayOfWeek: 1,
		dayNames: ["Pazar", "Pazartesi", "Salı", "Çarşamba", "Perşembe", "Cuma", "Cumartesi"],
		dayNamesShort: ["Paz", "Pts", "Sal", "Çar", "Per", "Cum", "Cts"],
		dayNamesMin: ["Pa", "Pt", "Sa", "Ça", "Pe", "Cu", "Ct"],
		monthNames: ["Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran", "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık"],
		monthNamesShort: ["Oca", "Şub", "Mar", "Nis", "May", "Haz", "Tem", "Ağu", "Eyl", "Eki", "Kas", "Ara"],
		today: 'Bugün',
		clear: 'Temizle'
	};
	baseForm: FormGroup;
	constructor(public messageService = new MessageService) {

	}

	ngOnInit() {
	}

	showErrors(response: IBaseResponse) {
		if (response) {
			if (response.errors != null && response.errors.length > 0) {
				for (var i = 0; i < response.errors.length; i++) {
					var error = response.errors[i];
					this.messageService.add({ severity: 'error', summary: 'İşlem Sırasında Hata', detail: error });
				}
			}
			else if (response.error) {
				this.messageService.add({ severity: 'error', summary: 'İşlem Sırasında Hata', detail: response.error });
			} else {
				this.messageService.add({ severity: 'error', summary: 'İşlem Sırasında Hata', detail: "Getting unhandled error" });
			}
		}
	}

	isValid(form?: any) {
		if (form) {
			return form.valid;
		}
		else {
			return this.baseForm.valid;
		}
	}

	cleanForm() {
		this.baseForm.reset();
	}

	dialogOnShowCenter(dialog) {
		setTimeout(() => { dialog.center.bind(dialog) }, 0)
	}
}
export abstract class BasePageComponentGenerics<T extends IBaseModel, ST extends BaseService> extends BasePageComponent {
	public rowData: T[];
	public record: T;
	public records: T[];
	constructor(private getType: new () => T, public service: ST, public messageService = new MessageService) {
		super(messageService)
		this.record = this.getNew();

	}

	getNew(): T {
		return new this.getType();
	}

	ngOnInit() {
	}

	public cleanData() {
		this.cleanForm();
		this.rowData = [];
	}
}