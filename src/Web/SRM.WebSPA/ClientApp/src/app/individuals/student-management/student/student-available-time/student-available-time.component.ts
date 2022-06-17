import { Component, Input } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MessageService } from 'prime/message/messageservice';
import { StudentReport, Hospital, StudentAvailableTime, DateCombination } from 'shared/models';
import { StudentReportService, HospitalService, StudentAvailableTimeService, StudentService } from 'shared/services';
import { StudentComponent } from 'individuals/student-management/student/student.component';
import { SelectItem, ConfirmationService } from 'primeng/primeng';
import { StudentBasePageComponent } from 'shared/components/basepages/student-basepage.component';

@Component({
	selector: 'student-available-time',
	templateUrl: './student-available-time.component.html'
})

export class StudentAvailableTimeComponent extends StudentBasePageComponent<StudentAvailableTime, StudentAvailableTimeService> {
	@Input() isSeperated: boolean;

	rowData: StudentAvailableTime[];
	studentId: number;
	studentIsIntegrated: string = "true";
	showIntegrated: boolean = true;
	constructor(
		public studentService: StudentService,
		public service: StudentAvailableTimeService,
		public messageService: MessageService,
		public confirmationService: ConfirmationService,
	) {
		super(StudentAvailableTime, studentService, service, messageService, confirmationService);
	}

	ngOnInit() {
		this.baseForm = new FormGroup({
			'isIntegrated': new FormControl('', Validators.required),
			'startDate': new FormControl('', Validators.required),
			'endDate': new FormControl('', Validators.required),
			'startTime': new FormControl(''),
			'endTime': new FormControl(''),
			'description': new FormControl(''),
			'monday': new FormControl(''),
			'tuesday': new FormControl(''),
			'wednesday': new FormControl(''),
			'thursday': new FormControl(''),
			'friday': new FormControl(''),
			'saturday': new FormControl(''),
			'sunday': new FormControl(''),
		});
		super.ngOnInit();
		this.record = new StudentAvailableTime();
	}

	initData(rowData: StudentAvailableTime[]) {
		for (let record of rowData) {
			if (record.startDate) {
				record.startDate = new Date(record.startDate);
			}
			if (record.endDate) {
				record.endDate = new Date(record.endDate);
			}
			if (record.startTime) {
				record.startTime = new Date(record.startTime);
			}
			if (record.endTime) {
				record.endTime = new Date(record.endTime);
			}
			// if(record.isIntegrated)
			// {
			// 	record.isIntegratedValue="1";
			// }
			// else{
			// 	record.isIntegrated
			// }
		}
		return rowData;
	}

	initAdd(record: StudentAvailableTime) {
		this.showIntegrated = true;
		this.studentIsIntegrated = "true"
	}

	beforeSave(record: any) {
		record.isIntegrated = this.studentIsIntegrated == "true";
		if (record.isIntegrated == false) {
			record.includedDate = {
				monday: record.monday,
				tuesday: record.tuesday,
				wednesday: record.wednesday,
				thursday: record.thursday,
				friday: record.friday,
				saturday: record.saturday,
				sunday: record.sunday,
			};
			// if (record.startTime && record.startTime != "")
			// 	record.startTime = record.startTime != null ? record.startTime.toISOString() : null;
			// if (record.endTime && record.endTime != "")
			// 	record.endTime = record.endTime != null ? record.endTime.toISOString() : null;
		}
	}

	afterSave(record: any) {
		record.isIntegrated = this.studentIsIntegrated == "true";
		if (record.isIntegrated == false) {
			record.includedDate = {
				monday: record.monday,
				tuesday: record.tuesday,
				wednesday: record.wednesday,
				thursday: record.thursday,
				friday: record.friday,
				saturday: record.saturday,
				sunday: record.sunday,
			};
			if (record.startTime) {
				record.startTime = new Date(record.startTime);
			}
			if (record.endTime) {
				record.endTime = new Date(record.endTime);
			}
			if (record.startTime && record.startTime != "")
				record.startTime = record.startTime != null ? record.startTime.toISOString() : null;
			if (record.endTime && record.endTime != "")
				record.endTime = record.endTime != null ? record.endTime.toISOString() : null;
		}
	}

	initEdit(record: StudentAvailableTime) {
		this.showIntegrated = this.record.isIntegrated;
		this.studentIsIntegrated = this.record.isIntegrated ? "true" : "false"
		if (!this.record.includedDate) {
			this.record.includedDate = new DateCombination();
		}
	}
	radioChecked(radioChecked) {
		if (radioChecked == "true") {
			this.showIntegrated = true;
		}
		else {
			this.showIntegrated = false;
		}
	}
	checkDisabledStatus() {
		if (this.isSeperated == true) {
			return false;
		}
		else if (this.studentService.isStudentClean != null) {
			return this.studentService.isStudentClean;
		}
	}
	showDialogToDelete(record: StudentAvailableTime, message?: any) {
		super.showDialogToDelete(record, "Kısıt türü " + (record.isIntegrated ? "Sürekli" : "Parçalı") + " ve tarih aralığı " + record.startDate.toLocaleDateString() + "-" + record.endDate.toLocaleDateString() + " olan kaydı silmek istiyor musunuz?");
	}

}
