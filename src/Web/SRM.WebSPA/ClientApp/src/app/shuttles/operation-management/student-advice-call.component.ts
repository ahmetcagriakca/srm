import { Component, Input } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MessageService } from 'prime/message/messageservice';
import { ShuttleService, StudentService, StudentCallService } from 'shared/services';
import { BasePageComponent } from 'shared/components';
import { ConfirmationService } from 'primeng/primeng';

@Component({
	selector: 'student-advice-call',
	templateUrl: './student-advice-call.component.html'
})

export class StudentAdviceCallComponent extends BasePageComponent {
	@Input() shuttleList: boolean;
	shuttleOperationId: any;
	id: number;
	rowData: any[];
	contacts: any[];

	studentAdviceDescription: string;
	studentShuttleInfo: any;
	student: any;
	displayAdviceDialog: boolean;
	constructor(
		public service: ShuttleService,
		public studentService: StudentService,
		public studentCallService: StudentCallService,
		public messageService: MessageService,
		public confirmationService: ConfirmationService,

	) {
		super(messageService);
	}

	ngOnInit() {
		this.displayAdviceDialog = false;
		super.ngOnInit();
		this.service.studentAdviceCallLoad$.subscribe(studentShuttleInfo => {
			this.studentShuttleInfo = studentShuttleInfo;
			this.loadData();
		});
	}

	setInfo(result: any, isAfterLoad?: boolean) {
		if (result.id > 0) {
			this.displayAdviceDialog = true;
			this.studentAdviceDescription = "";
			this.student = result;
			this.student.studentName = this.student.name + " " + this.student.surname;
		}
		else {
			if (isAfterLoad == false)
				this.messageService.add({ severity: 'warn', summary: 'Uyarı', detail: 'Gösterilecek öneri bulunamadı' });
			this.displayAdviceDialog = false;
		}
	}

	loadData(isAfterLoad?: boolean) {
		this.getStudentInformation(this.studentShuttleInfo.advice.studentId, isAfterLoad)
	}

	getStudentInformation(studentId, isAfterLoad?: boolean) {
		this.studentService.getStudentById(studentId)
			.subscribe(response => {
				this.setInfo(response.resultValue, isAfterLoad);
			});
	}

	studentAnswer(answer) {
		this.confirmationService.confirm({
			message: 'Öğrencinin cevabını ' + (answer ? 'Onay' : 'Red') + ' olarak işaretlemek istiyor musunuz?',
			header: 'Arama Cevapla',
			icon: 'pi pi-question-circle',
			accept: () => {
				var request = {
					operationId: this.studentShuttleInfo.operationId,
					studentId: this.studentShuttleInfo.advice.studentId,
					studentAnswer: answer,
					callType: 2,
					description: this.studentAdviceDescription
				}
				this.studentCallService.saveStudentCall(request)
					.subscribe(response => {
						if (response.isSuccess) {
							this.messageService.add({ severity: 'success', summary: 'İşlem Başarılı', detail: this.student.studentName + ' cevabı ' + (answer == true ? "geliyorum" : "gelmiyorum") + ' olarak kaydedildi.' });
							this.displayAdviceDialog = false;
							if (answer == 1) {
								this.confirmationService.confirm({
									message: 'Öğrencinin cevabı red olarak işaretlendi, Kısıt eklemek istiyor musunuz? Eklememeniz durumunda öğrenci önerisi gösterilmeye devam edilecektir.',
									header: 'Kısıt ekle',
									icon: 'pi pi-question-circle',
									accept: () => {
										this.service.studentAvailableTimeShowSource.next(this.studentShuttleInfo.advice.studentId);
									},
									reject: () => {
									}
								});
							}
										this.service.studentAdviceFinishSource.next();
									} else {
							this.showErrors(response);
						}
					});
			},
			reject: () => {
			}
		});
	}

	cancel() {
		// this.record = this.getNew();
		this.displayAdviceDialog = false;
	}
}
