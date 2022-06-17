import { Component, Input } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MessageService } from 'prime/message/messageservice';
import {  StudentContact } from 'shared/models';
import {  StudentContactService, StudentService } from 'shared/services';
import { StudentComponent } from 'individuals/student-management/student/student.component';
import { SelectItem, ConfirmationService } from 'primeng/primeng';
import { StudentBasePageComponent } from 'shared/components/basepages/student-basepage.component';

@Component({
	selector: 'student-contact',
	templateUrl: './student-contact.component.html'
})

export class StudentContactComponent extends StudentBasePageComponent<StudentContact, StudentContactService> {
	@Input() isSeperated: boolean;

	rowData: StudentContact[];
	studentId: number;
	studentIsIntegrated: string = "true";
	showIntegrated: boolean = true;
	constructor(
		public studentService: StudentService,
		public service: StudentContactService,
		public messageService: MessageService,
		public confirmationService: ConfirmationService,
	) {
		super(StudentContact, studentService, service, messageService, confirmationService);
	}

	ngOnInit() {
		this.baseForm = new FormGroup({
			'Name': new FormControl('', Validators.required),
			'Number': new FormControl('', Validators.required)			
		});
		super.ngOnInit();
		this.record = new StudentContact();
	}

	initData(rowData: StudentContact[]) {
		// for (let record of rowData) {
		
		// }
		return rowData;
	}

	initAdd(record: StudentContact) {
	
	}

	 beforeSave(record: any) {
	// 	record.isIntegrated = this.studentIsIntegrated == "true";
	// 	if (record.isIntegrated == false) {
	// 		record.includedDate = {
	// 			monday: record.monday,
	// 			tuesday: record.tuesday,
	// 			wednesday: record.wednesday,
	// 			thursday: record.thursday,
	// 			friday: record.friday,
	// 			saturday: record.saturday,
	// 			sunday: record.sunday,
	// 		};
	// 		// if (record.startTime && record.startTime != "")
	// 		// 	record.startTime = record.startTime != null ? record.startTime.toISOString() : null;
	// 		// if (record.endTime && record.endTime != "")
	// 		// 	record.endTime = record.endTime != null ? record.endTime.toISOString() : null;
	// 	}
	  }

  afterSave(record: any) {
	// 	record.isIntegrated = this.studentIsIntegrated == "true";
	// 	if (record.isIntegrated == false) {
	// 		record.includedDate = {
	// 			monday: record.monday,
	// 			tuesday: record.tuesday,
	// 			wednesday: record.wednesday,
	// 			thursday: record.thursday,
	// 			friday: record.friday,
	// 			saturday: record.saturday,
	// 			sunday: record.sunday,
	// 		};
	// 		if (record.startTime) {
	// 			record.startTime = new Date(record.startTime);
	// 		}
	// 		if (record.endTime) {
	// 			record.endTime = new Date(record.endTime);
	// 		}
	// 		if (record.startTime && record.startTime != "")
	// 			record.startTime = record.startTime != null ? record.startTime.toISOString() : null;
	// 		if (record.endTime && record.endTime != "")
	// 			record.endTime = record.endTime != null ? record.endTime.toISOString() : null;
	// 	}
	  }

	//  initEdit(record: StudentContact) {
	// 	this.showIntegrated = this.record.isIntegrated;
	// 	this.studentIsIntegrated = this.record.isIntegrated ? "true" : "false"
	// 	if (!this.record.includedDate) {
	// 		this.record.includedDate = new DateCombination();
	// 	}
	// }
	// radioChecked(radioChecked) {
	// 	if (radioChecked == "true") {
	// 		this.showIntegrated = true;
	// 	}
	// 	else {
	// 		this.showIntegrated = false;
	// 	}
	//  }
	checkDisabledStatus() {
		if (this.isSeperated == true) {
			return false;
		}
		else if (this.studentService.isStudentClean != null) {
			return this.studentService.isStudentClean;
		}
	}
	showDialogToDelete(record: StudentContact, message?: any) {
		super.showDialogToDelete(record,record.name +  " velisinin " + record.number+ " numaralı iletişim bilgisini silmek istiyor musunuz?");
	}

}
