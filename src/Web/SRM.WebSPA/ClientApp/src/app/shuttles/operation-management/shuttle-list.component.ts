import { Component } from '@angular/core';
import { MessageService } from 'prime/message/messageservice';
import { BasePageComponent } from 'shared/components';
import { ShuttleService, ExcelService, StudentService } from 'shared/services';
import { Student } from 'shared/models';
import { SelectItem } from 'primeng/primeng';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
	selector: 'shuttle-list',
	templateUrl: './shuttle-list.component.html'
})

export class ShuttleListComponent extends BasePageComponent {
	displayShuttleDialog: boolean;
	id: number;
	rowData: any[];
	searchForm: FormGroup;
	shuttleOperationDate: Date;
	searchedShuttleOperationDate: Date;
	CompletedLessonCounts: SelectItem[];
	AddStudenCompletedLessonCounts: SelectItem[];
	lessonCount: number;
	lastRecord: any;
	constructor(public service: ShuttleService,
		public studentService: StudentService,
		public messageService: MessageService,
		public excelService: ExcelService,
	) {
		super(messageService);
	}

	ngOnInit() {
		super.ngOnInit();
		this.shuttleOperationDate = new Date();
		this.loadData();
		this.searchForm = new FormGroup({
			'students': new FormControl('', Validators.required),
			'lessonCount': new FormControl('', Validators.required),

		});
		this.Students = [{ label: "Seçiniz", value: null }];
		this.studentService.getStudents()
			.subscribe(response => {
				var entities = (<Student[]>response.resultValue);
				for (let entity of entities) {
					this.Students.push({ label: entity.name + " " + entity.surname + "(" + entity.identityNumber + ")", value: entity.id });
				}
			});
		this.CompletedLessonCounts = [];
		for (let index = 0; index < 3; index++) {
			var lessonCount = index.toString();
			this.CompletedLessonCounts.push({ label: lessonCount, value: lessonCount });
		}
		this.AddStudenCompletedLessonCounts = [{ label: "Seçiniz", value: null }];
		for (let index = 1; index < 3; index++) {
			var lessonCount = index.toString();
			this.AddStudenCompletedLessonCounts.push({ label: lessonCount, value: lessonCount });
		}

		this.service.shuttleListLoad$.subscribe((callObject: any) => {
			this.loadData();
		});
		this.service.shuttleStudentStatusUpdate$.subscribe((status: any) => {
			if (status) {
				if (this.lastRecord) {
					this.lastRecord.status = status;
					this.setStatusValues(this.lastRecord);
				}
			}
		});
	}

	public setStatusValues(student) {
		switch (student.status) {
			case 0:
				student.statusValue = "0";
				student.statusDescription = 'Aranmadı';
				break;
			case 1:
				student.statusValue = "1";
				student.statusDescription = 'Geldi';
				break;
			case 2:
				student.statusValue = "2";
				student.statusDescription = 'Gelmedi';
				break;
			case 3:
				student.statusValue = "3";
				student.statusDescription = 'Gelecek';
				break;
			case 4:
				student.statusValue = "4";
				student.statusDescription = 'Gelmeyecek';
				break;
			default:
				break;
		}
	}

	setInfo(rowData: any[]) {
		var x = rowData;
		if (x && x.length > 0) {
			for (let row of x) {
				//TODO:ShuttleOperationStatus olarak değiştiridim.value ları bi kontrol etmek lazım.
				let counterCame: number = 0;
				let counterNotCame: number = 0;

				if (row.dateTime) {
					row.dateTime = new Date(row.dateTime);
				}
				for (let student of row.students) {
					student.lessonRelation.completedLessonCount = student.lessonRelation.completedLessonCount.toString();
					student.lessonRelation.completedLessonCountOld = student.lessonRelation.completedLessonCount;
					switch (student.status) {
						case 0:
							student.statusValue = "0";
							student.statusDescription = 'Aranmadı';
							break;
						case 1:
							student.statusValue = "1";
							student.statusDescription = 'Geldi';
							counterCame++;
							break;
						case 2:
							student.statusValue = "2";
							student.statusDescription = 'Gelmedi';
							counterNotCame++;
							break;
						case 3:
							student.statusValue = "3";
							student.statusDescription = 'Gelecek';
							break;
						case 4:
							if (row.operationStatus == 1) {
								student.statusValue = "2";
								student.statusDescription = 'Gelmedi';
							} else {
								student.statusValue = "4";
								student.statusDescription = 'Gelmeyecek';
							}
							break;
						default:
							break;
					}

				}
				row.counterCame = counterCame;
				row.counterNotCame = counterNotCame;
				row.statusMessage = row.operationStatus == 0 ? ("Servis Başlamadı") : (row.operationStatus == 1 ? "Servis Başladı" : "Tamamlandı");
			}
			this.rowData = x;
		}
		else {
			this.rowData = [];
		}
	}

	loadData() {
		this.getShuttles();
	}

	getShuttles() {
		this.searchedShuttleOperationDate = this.shuttleOperationDate;
		var searchDateString = this.shuttleOperationDate != null ? this.shuttleOperationDate.toISOString() : null;
		var requestObject = { date: searchDateString }
		this.service.getStudentOperationListByDate(requestObject)
			.subscribe(response => {
				this.setInfo(response.resultValue);
			});
	}

	openShuttleAdvice(id) {
		this.service.shuttleAdviceByIdLoadSource.next(id);
		this.displayShuttleDialog = true;

	}

	radioChecked(operationStatus, studentName, shuttleStudentOperationsId, statusValue, record) {
		if (operationStatus == "1" || operationStatus == "2") {
			if (statusValue == "1" || statusValue == "2") {
				var requestObject = {
					studentShuttleOperationId: shuttleStudentOperationsId,
					studentOperasionStatus: statusValue
				}
				this.service.setStudentShuttleOperationStatus(requestObject)
					.subscribe(response => {
						if (response.isSuccess) {
							record.status = statusValue;
							this.messageService.add({ severity: 'success', summary: 'İşlem Başarılı', detail: studentName + ' ' + (statusValue == '1' ? "geldi" : "gelmedi") + ' olarak kaydedildi.' });
						} else {
							this.showErrors(response);
						}
					});
			}
			else {
				this.messageService.add({ severity: 'warn', summary: 'Uyarı', detail: 'Hatalı öğrenci durum girişi. ' + statusValue });

			}
		}
		else {
			this.messageService.add({ severity: 'warn', summary: 'Uyarı', detail: 'Servis işlemleri ' + (operationStatus == 2 ? 'tamamlanmış' : 'başlamamış') + ' işlem yapılamaz.' });

		}
	}
	formatDate(dDate, sMode) {
		var today = dDate;
		var dd = today.getDate();
		var mm = today.getMonth() + 1; //January is 0!
		var yyyy = today.getFullYear();
		if (dd < 10) {
			dd = '0' + dd
		}
		if (mm < 10) {
			mm = '0' + mm
		}
		if (sMode + "" == "") {
			sMode = "dd/mm/yyyy";
		}
		if (sMode == "dd-mm-yyyy") {
			return dd + "-" + mm + "-" + yyyy + "";
		}
		if (sMode == "yyyy-mm-dd") {
			return yyyy + "-" + mm + "-" + dd + "";
		}
		if (sMode == "dd/mm/yyyy") {
			return dd + "/" + mm + "/" + yyyy;
		}
	}

	exportAsXLSX(element?: any): void {
		if (this.rowData != null && this.rowData.length) {
			if (this.shuttleOperationDate) {
				var excelDatas = []
				if (element) {
					var row = this.createExcelData(element);
					excelDatas.push(row);
				}
				else {
					for (let index = 0; index < this.rowData.length; index++) {
						const element = this.rowData[index];
						var row = this.createExcelData(element);
						excelDatas.push(row);
					}
				}

				this.excelService.exportAsExcelFileWithSubArray(excelDatas, "Öğrenciler", ["TarihFormatted", "Bölge Adı",], ["TarihFormatted", "Öğrenciler"], 'ServisListesi_' + this.formatDate(this.shuttleOperationDate, "dd-mm-yyyy"));
			}
			else {
				this.messageService.add({ severity: 'warn', summary: 'Uyarı', detail: 'Tarih boş geçilemez.' });
			}
		}
		else {
			this.messageService.add({ severity: 'warn', summary: 'Uyarı', detail: 'Çıktı alınacak kayıt bulunamadı.' });
		}
	}

	private createExcelData(element: any) {
		element.dateTime = new Date(element.dateTime);
		var dateString = element.dateTime.toLocaleDateString() + "-" + element.dateTime.toLocaleTimeString().substr(0, 5);
		var dateStringFormatted = element.dateTime.toLocaleTimeString().substr(0, 2) + "-" + element.dateTime.toLocaleTimeString().substr(3, 2);
		var excelData = {
			"Tarih": dateString,
			"Bölge Adı": element.regionName,
			"Durum": element.statusMessage,
			"TarihFormatted": dateStringFormatted
		};
		var students = [];
		for (let j = 0; j < element.students.length; j++) {
			const student = element.students[j];
			students.push({
				"Öğrenci Adı": student.name,
				"Durum": student.statusValue == 1 ? "Geldi" : (student.statusValue == 2 ? "Gelmedi" : ""),
				"Katılım Türü": student.isCompensation ? "Telafi" : "Planlı",
			});
		}
		excelData["Öğrenciler"] = students;
		return excelData;
	}

	changeOperationStatus(shuttleOperationId, status) {
		if (status == 1 || status == 2) {
			var requestObject = {
				shuttleOperationId: shuttleOperationId,
				status: status
			}
			this.service.setShuttleOperationStatus(requestObject)
				.subscribe(response => {
					if (response.isSuccess) {
						this.messageService.add({ severity: 'success', summary: 'İşlem Başarılı', detail: 'Servis operasyonu ' + (status == 1 ? 'başladı' : 'bitti') });
						this.loadData();
					} else {
						this.showErrors(response);
					}
				});
		}
		else {
			this.messageService.add({ severity: 'warn', summary: 'Uyarı', detail: 'Durum başla ve bitir dışında değiştirilemez.' });
			return;
		}
	}
	changeOperationStatusButtonText(status) {
		return status == 0 ? 'Operasyon Başlat' : 'Operasyon Bitir';
	}
	displayShuttleLocationDialog: boolean = false;
	showOperationLocations(shuttle) {
		this.displayShuttleLocationDialog = true;
		this.service.shuttleOperationLocationSource.next(shuttle.shuttleOperationId);
	}

	displayAddStudent: boolean = false;
	addStudentSelectedShuttle: any;
	public Students: SelectItem[];
	public selectedStudent: number;
	showAddStudentDialog(shuttle) {
		this.displayAddStudent = true;
		this.addStudentSelectedShuttle = shuttle;
	}

	addNewStudent() {
		if (this.selectedStudent) {
			var requestObject = {
				studentId: this.selectedStudent,
				shuttleOperationId: this.addStudentSelectedShuttle.shuttleOperationId,
				lessonCount: this.lessonCount
			}
			this.service.createCustomStudentOperation(requestObject)
				.subscribe(response => {
					if (response.isSuccess) {
						this.messageService.add({ severity: 'success', summary: 'İşlem Başarılı', detail: 'Öğrenci Servis Operasyonuna eklendi' });
						this.loadData();
						this.selectedStudent = null;
						this.displayAddStudent = false;
					} else {
						this.showErrors(response);
					}
				});
		}
		else {
			this.messageService.add({ severity: 'warn', summary: 'Öğrenci seçimi yapılmadı', detail: 'Eklemeden önce öğrenci seçimi yapılması gerekiyor.' });
		}
	}
	startStudentCall(shuttle, shuttleStudentOperationId, studentId, record) {
		var callStart = {
			shuttle: shuttle,
			shuttleStudentOperationId: shuttleStudentOperationId,
			studentId: studentId
		};
		this.lastRecord = record;
		this.service.studentCallLoadedSource.next(callStart);

	}


	saveCallOperation(operationStatus, studentName, shuttleStudentOperationsId, statusValue) {
		if (operationStatus == "0") {
			if (statusValue == "3" || statusValue == "4") {
				var requestObject = {
					studentShuttleOperationId: shuttleStudentOperationsId,
					studentOperasionStatus: statusValue
				}
				this.service.setStudentShuttleOperationStatus(requestObject)
					.subscribe(response => {
						if (response.isSuccess) {
							this.messageService.add({ severity: 'success', summary: 'İşlem Başarılı', detail: studentName + ' ' + (statusValue == '3' ? "gelecek" : "gelmeyecek") + ' olarak kaydedildi.' });
						} else {
							this.showErrors(response);
						}
					});
			}
			else {
				this.messageService.add({ severity: 'warn', summary: 'Uyarı', detail: 'Hatalı arama durum girişi. ' + statusValue });

			}
		}
		else {
			this.messageService.add({ severity: 'warn', summary: 'Uyarı', detail: 'Servis işlemleri ' + (operationStatus == 2 ? 'tamamlanmış' : 'başlamış') + ' işlem yapılamaz.' });

		}
	}

	setStudentOperastionLessonsCount(record) {


		var requestObject = {
			shuttleStudentOperationId: record.shuttleStudentOperationId,
			complatedLessonCount: record.lessonRelation.completedLessonCount
		}
		this.service.setStudentOperastionLessonsCount(requestObject)
			.subscribe(response => {
				if (response.isSuccess) {
					this.messageService.add({ severity: 'success', summary: 'İşlem Başarılı', detail: 'Öğrencinin katıldığı ders sayısı ' + record.lessonRelation.completedLessonCount + ' olarak güncellendi.' });
					record.lessonRelation.completedLessonCountOld = record.lessonRelation.completedLessonCount;
				} else {
					this.showErrors(response);
				}
			});
	}

	checkDate(date) {
		var newDate = new Date();
		var currentDate = new Date(newDate.getFullYear(), newDate.getMonth(), newDate.getDate())
		return date < currentDate;
	}
}