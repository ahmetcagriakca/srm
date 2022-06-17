import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MessageService } from 'prime/message/messageservice';
import { StudentReport, Hospital } from 'shared/models';
import { StudentReportService, HospitalService, StudentService } from 'shared/services';
import { StudentComponent } from 'individuals/student-management/student/student.component';
import { SelectItem, ConfirmationService } from 'primeng/primeng';
import { StudentBasePageComponent } from 'shared/components/basepages/student-basepage.component';

@Component({
	selector: 'student-report',
	templateUrl: './student-report.component.html'
})

export class StudentReportComponent extends StudentBasePageComponent<StudentReport, StudentReportService> {
	rowData: StudentReport[];
	studentId: number;

	public Hospitals: SelectItem[];
	public selectedHospital: number;

	constructor(
		public studentService: StudentService,
		public service: StudentReportService,
		public hospitalService: HospitalService,
		public messageService: MessageService,
		public confirmationService: ConfirmationService,
	) {
		super(StudentReport, studentService, service, messageService, confirmationService);
	}

	ngOnInit() {
		this.baseForm = new FormGroup({
			'reportNumber': new FormControl('', Validators.required),
			'givenHospital': new FormControl('', Validators.required),
			'startDate': new FormControl('', Validators.required),
			'endDate': new FormControl('', Validators.required),
			'content': new FormControl('', Validators.required),
		});
		super.ngOnInit();

		this.Hospitals = [];
		this.Hospitals = [{ label: "Seçiniz", value: null }];
		this.hospitalService.get()
			.subscribe(response => {
				var entities = (<Hospital[]>response.resultValue);
				for (let entity of entities) {
					this.Hospitals.push({ label: entity.name, value: entity.id });
				}
			});
		this.record = new StudentReport();
		this.record.givenHospital = new Hospital();
	}

	initData(rowData: StudentReport[]) {
		for (let record of rowData) {
			if (record.startDate) {
				record.startDate = new Date(record.startDate);
			}
			if (record.endDate) {
				record.endDate = new Date(record.endDate);
			}
		}
		return rowData;
	}

	initAdd(record: StudentReport) {
		this.selectedHospital = null;
	}

	initEdit(record: StudentReport) {
		if (record.givenHospital) {
			this.selectedHospital = record.givenHospital.id;
		}
	}
}
