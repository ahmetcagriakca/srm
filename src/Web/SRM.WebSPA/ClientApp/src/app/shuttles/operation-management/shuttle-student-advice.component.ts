import { Component, Input } from '@angular/core';
import { MessageService } from 'prime/message/messageservice';
import { ShuttleService, StudentService } from 'shared/services';
import { BasePageComponent } from 'shared/components';
import { ConfirmationService } from 'primeng/primeng';

@Component({
	selector: 'shuttle-student-advice',
	templateUrl: './shuttle-student-advice.component.html'
})

export class ShuttleStudentAdviceComponent extends BasePageComponent {

	@Input() shuttleList: boolean;
	shuttleOperationId: any;
	id: number;
	rowData: any;
	shuttleOperationDate: Date;
	searchedShuttleOperationDate: Date;
	showDateSearch: boolean = true;
	displayStudentAvailableDialog: boolean = false;

	constructor(
		public service: ShuttleService,
		public messageService: MessageService,
		public confirmationService: ConfirmationService,
		public studentService: StudentService,

	) {
		super(messageService);


	}

	ngOnInit() {
		if (this.showDateSearch == true) {
			this.shuttleOperationDate = new Date();
			this.showDateSearch = true;
			this.loadData(false);
		}
		super.ngOnInit();
		//öğrenci arama sonucunda tekrar load etme işlemi yapılıyor.
		this.service.studentCallComplete$.subscribe((result: any) => {
			this.loadData(true);
			if (result.answer == false) {
				this.confirmationService.confirm({
					message: 'Öğrencinin cevabı red olarak işaretlendi, Kısıt eklemek istiyor musunuz?Eklememeniz durumunda öğrenci önerisi gösterilmeye devam edilecek',
					header: 'Kısıt ekle',
					icon: 'pi pi-question-circle',
					accept: () => {
						this.service.getStudentShuttleAdviceById(result.adviceId)
							.subscribe(response => {
								if (response.isSuccess) {
									if (response.resultValue) {
										this.service.studentAvailableTimeShowSource.next(response.resultValue.studentId);
									}
								}
							});
					},
					reject: () => {
					}
				});
			}
			else {
			}
		});
		this.service.studentAvailableTimeShow$.subscribe((studentId: any) => {
			this.displayStudentAvailableDialog = true;
			this.studentService.studentLoadedSource.next(studentId);
		});

	}

	setInfo(rowData) {
		this.rowData = rowData;
		for (const row of this.rowData) {
			
			if (row.dateTime) {
				row.dateTime = new Date(row.dateTime);
			}
		}

	}
	loadData(isAfterLoad: boolean) {
		this.getStudentShuttleCallList()
	}

	getStudentShuttleCallList() {
		var searchDateString = this.shuttleOperationDate != null ? this.shuttleOperationDate.toISOString() : null;
		var requestObject = { date: searchDateString };
		this.service.getStudentShuttleCallList(requestObject)
			.subscribe(response => {
				this.setInfo(response.resultValue);
			});
	}

	startStudenCall(advice) {
		this.service.studentAdviceCallLoadedSource.next(advice);
	}

	createDailyAdvice() {
		this.confirmationService.confirm({
			message: this.shuttleOperationDate.toLocaleDateString() + ' için öneri oluşturulacaktır, onaylıyor musunuz?',
			header: 'Öneri Oluştur Onay',
			icon: 'pi pi-question-circle',
			accept: () => {
				var dateString = this.shuttleOperationDate != null ? this.shuttleOperationDate.toISOString() : null;
				var requestObject = { date: dateString };
				this.service.createDailyAdvice(null, requestObject)
					.subscribe(response => {
						this.messageService.add({ severity: 'success', summary: 'İşlem Başarılı', detail: 'İşlem Tamamlandı.' });
						this.loadData(false);
					});
			},
			reject: () => {
			}
		});
	}

	checkAdviceCreate(dateTime) {
		var currentDate = new Date();
		currentDate.setDate(currentDate.getDate() - 1);
		return dateTime < currentDate;
	}
}
