import { Component, Input } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MessageService } from 'prime/message/messageservice';
import { ShuttleService, StudentService } from 'shared/services';
import { BasePageComponent } from 'shared/components';
import { ConfirmationService } from 'primeng/primeng';

@Component({
	selector: 'student-call',
	templateUrl: './student-call.component.html'
})

export class StudentCallComponent extends BasePageComponent {
	@Input() shuttleList: boolean;
	shuttleOperationId: any;
	id: number;
	rowData: any[];

	studentCallDescription: string;
	selectedStudent: any;
	selectedShuttle: any;
	selectedAdvice: any;
	displayStudentCallDialog: boolean;
	shuttleStudentOperationId: any;
	constructor(
		public service: ShuttleService,
		public studentService: StudentService,
		public messageService: MessageService,
		public confirmationService: ConfirmationService,
	) {
		super(messageService);
	}

	ngOnInit() {
		this.displayStudentCallDialog = false;
		super.ngOnInit();
		this.service.studentCallLoad$.subscribe((callObject: any) => {
			this.loadData(callObject);
		});
	}
	loadData(callObject, isAfterLoad?: boolean) {
		this.displayStudentCallDialog = true;
		this.studentCallDescription = "";
		this.selectedShuttle = callObject.shuttle;
		
		if (this.selectedShuttle.dateTime) {
			this.selectedShuttle.dateTime = new Date(this.selectedShuttle.dateTime);
		}
		this.shuttleStudentOperationId = callObject.shuttleStudentOperationId;
		this.studentService.getStudentById(callObject.studentId)
			.subscribe(response => {
				this.selectedStudent = response.resultValue;
			});
	}

	cancel() {
		// this.record = this.getNew();
		this.displayStudentCallDialog = false;
	}


	saveCallOperation(studentName, statusValue) {
		if (statusValue == "3" || statusValue == "4") {
			var requestObject = {
				studentShuttleOperationId: this.shuttleStudentOperationId,
				studentOperasionStatus: statusValue
			}

			this.confirmationService.confirm({
				message: 'Öğrencinin cevabını ' + (statusValue == 3 ? 'Gelecek' : 'Gelmeyecek') + ' olarak işaretlemek istiyor musunuz?',
				header: 'Arama Cevap',
				icon: 'pi pi-question-circle',
				accept: () => {
					this.service.setStudentShuttleOperationStatus(requestObject)
						.subscribe(response => {
							if (response.isSuccess) {
								this.messageService.add({ severity: 'success', summary: 'İşlem Başarılı', detail: studentName + ' ' + (statusValue == '3' ? "gelecek" : "gelmeyecek") + ' olarak kaydedildi.' });
								this.service.shuttleStudentStatusUpdateSource.next(statusValue);
								this.cancel();
								// this.displayStudentAvailableDialog = true;
								if (statusValue == "4") {
									this.confirmationService.confirm({
										message: 'Öğrencinin cevabı gelmeyecek olarak işaretlendi, Kısıt eklemek istiyor musunuz?',
										header: 'Kısıt ekle',
										icon: 'pi pi-question-circle',
										accept: () => {
											this.service.studentAvailableTimeShowSource.next(this.selectedStudent.id);
										},
										reject: () => {
										}
									});
								}
							} else {
								this.showErrors(response);
							}
						});
				},
				reject: () => {
				}
			});
		}
		else {
			this.messageService.add({ severity: 'warn', summary: 'Uyarı', detail: 'Hatalı arama durum girişi. ' + statusValue });
		}
	}


	checkDate() {
		var newDate = new Date();
		var currentDate = new Date(newDate.getFullYear(), newDate.getMonth(), newDate.getDate())
		if (this.selectedShuttle) {
			return this.selectedShuttle.dateTime < currentDate;
		}
		else {
			return false;
		}
	}
}
