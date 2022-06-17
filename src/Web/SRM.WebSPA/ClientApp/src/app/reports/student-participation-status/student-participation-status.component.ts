import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MessageService } from 'prime/message/messageservice';
import { BasePageComponent } from 'shared/components';
import { LocationRegion } from 'shared/models';
import { LocationRegionService, ReportService, ExcelService } from 'shared/services';
import { SelectItem, ConfirmationService } from 'primeng/primeng';

@Component({
	selector: 'student-participation-status',
	templateUrl: './student-participation-status.component.html'
})

export class StudentParticipationStatusReport extends BasePageComponent {
	studentJoinStatus: any;
	studentJoinStatusRange: Date[];
	reportContent: any;
	rowData: any[];

	yearFilter: number[] = [0, 100];
	yearGtFilter: number;
	yearLtFilter: number;
	cols: any[];
	constructor(
		public service: ReportService,
		public messageService: MessageService,
		public excelService: ExcelService,

	) {
		super(messageService);
	}

	ngOnInit() {
		var searchDateString = new Date().toISOString();
		var date = new Date();
		this.studentJoinStatusRange = [
			new Date(date.getFullYear(), date.getMonth(), 1),
			new Date(date.getFullYear(), date.getMonth(), date.getDate()),
		]
		this.searchStudentJoinStatus();
		this.cols = [
			{ field: 'studentId', header: 'Öğrenci No' },
			{ field: 'studentIdentityNo', header: 'Kimlik No' },
			{ field: 'studentName', header: 'Öğrenci Adı' },
			{ field: 'plannedLessonCount', header: 'Planlanan Ders' },
			{ field: 'compensationLessonCount', header: 'Telafi Ders Sayısı' },
			{ field: 'joinedLessonCount', header: 'Katıldığı Ders Sayısı' },
			{ field: 'notJoinedLessonCount', header: 'Katılmadığı Ders Sayısı' },
			{ field: 'participationPercentageString', header: 'Katılım Yüzdesi %', sortField: 'participationPercentage' }
		];
		for (let index = 0; index < this.cols.length; index++) {
			const element = this.cols[index];
			if (!element.sortField) {
				element.sortField = element.field;
			}
		}
	}


	searchStudentJoinStatus() {
		this.rowData = [];
		if (this.studentJoinStatusRange && this.studentJoinStatusRange.length > 0) {
			var startDateString = this.studentJoinStatusRange[0].toISOString();
			if (this.studentJoinStatusRange.length > 1 && this.studentJoinStatusRange[1]) {
				var endDateString = this.studentJoinStatusRange[1].toISOString();
			}
			var request = {
				startDate: startDateString,
				endDate: endDateString
			};
			this.service.getStudentJoinStatus(request)
				.subscribe(response => {
					this.reportContent = response.resultValue;
					if (this.reportContent.totalParticipationPercentage) {
						this.reportContent.totalParticipationPercentage = "%" + this.reportContent.totalParticipationPercentage.toFixed(2);
					}
					for (let index = 0; index < this.reportContent.list.length; index++) {
						const element = this.reportContent.list[index];
						element.participationPercentageString = "%" + element.participationPercentage.toFixed(2);
					}
					this.rowData = this.reportContent.list;
				});
		}
		else {

		}
	}
	yearTimeout: any;

	filterClear(dt) {
		this.yearFilter = [0, 100];

		if (this.yearTimeout) {
			clearTimeout(this.yearTimeout);
		}

		this.yearTimeout = setTimeout(() => {
			dt.filter(this.yearFilter, 'participationPercentage', 'range');
			// if (isGt) {
			// 	dt.filter(event.value, 'participationPercentage', 'gte');
			// }
			// else {
			// 	dt.filter(event.value, 'participationPercentage', 'lte');
			// }
		}, 250);
	}
	onYearChange(event, dt, isGt) {
		if (this.yearTimeout) {
			clearTimeout(this.yearTimeout);
		}

		this.yearTimeout = setTimeout(() => {
			dt.filter(event.values, 'participationPercentage', 'range');
			// if (isGt) {
			// 	dt.filter(event.value, 'participationPercentage', 'gte');
			// }
			// else {
			// 	dt.filter(event.value, 'participationPercentage', 'lte');
			// }
		}, 250);
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

	exportAsXLSX(): void {
		if (this.rowData != null && this.rowData.length) {
			var excelDatas = this.createExcelData(this.rowData);
			// excelDatas.push(row);
			var startDateString = this.formatDate(this.studentJoinStatusRange[0], "dd-mm-yyyy");
			if (this.studentJoinStatusRange.length > 1 && this.studentJoinStatusRange[1]) {
				var endDateString = this.formatDate(this.studentJoinStatusRange[1], "dd-mm-yyyy");
			}
			this.excelService.exportAsExcelFile(excelDatas, "ÖğrenciKatılımDurum_" + startDateString + "-" + endDateString);
		}
		else {
			this.messageService.add({ severity: 'warn', summary: 'Uyarı', detail: 'Çıktı alınacak kayıt bulunamadı.' });
		}
	}


// 	<srm-formcontrol text="Toplam Planlanan Ders Sayısı" for="name">
// 	{{reportContent?.totalPlannedLessonCount}}
// </srm-formcontrol>
// <srm-formcontrol text="Toplam Telafi Sayısı" for="name">
// 	{{reportContent?.totalCompensationLessonCount}}
// </srm-formcontrol>
// <srm-formcontrol text="Öğrencilerin Katıldığı Toplam Ders Sayısı" for="name">
// 	{{reportContent?.totalJoinedLessonCount}}
// </srm-formcontrol>
// <srm-formcontrol text="Öğrencilerin Katılmadığı Toplam Ders Sayısı" for="name">
// 	{{reportContent?.totalNotJoinedLessonCount}}
// </srm-formcontrol>
// <srm-formcontrol text="Öğrencilerin Toplam Katılım Yüzdesi" for="name">
//    {{reportContent?.totalParticipationPercentage}}
// </srm-formcontrol>
// 	private createExcelData(element: any) {
// 		element.dateTime = new Date(element.dateTime);
// 		var dateString = element.dateTime.toLocaleDateString() + "-" + element.dateTime.toLocaleTimeString().substr(0, 5);
// 		var dateStringFormatted = element.dateTime.toLocaleTimeString().substr(0, 2) + "-" + element.dateTime.toLocaleTimeString().substr(3, 2);
// 		var excelData = {
// 			"Tarih": dateString,
// 			"Bölge Adı": element.regionName,
// 			"Durum": element.statusMessage,
// 			"TarihFormatted": dateStringFormatted
// 		};
// 		var students = [];
// 		for (let j = 0; j < element.students.length; j++) {
// 			const student = element.students[j];
// 			students.push({
// 				"Öğrenci Adı": student.name,
// 				"Durum": student.statusValue == 1 ? "Geldi" : (student.statusValue == 2 ? "Gelmedi" : ""),
// 				"Katılım Türü": student.isCompensation ? "Telafi" : "Planlı",
// 			});
// 		}
// 		excelData["Öğrenciler"] = students;
// 		return excelData;
// 	}

	private createExcelData(rowData: any[]) {
		// var excelData = {
		// 	'Öğrenci No' : element.studentId,
		// 	'Kimlik No' : element.studentIdentityNo,
		// 	'Öğrenci Adı' : element.studentName,
		// 	'Planlanan Ders' : element.plannedLessonCount,
		// 	'Telafi Ders Sayısı' : element.compensationLessonCount,
		// 	'Katıldığı Ders Sayısı' : element.joinedLessonCount,
		// 	'Katılmadığı Ders Sayısı' : element.notJoinedLessonCount,
		// 	'Katılım Yüzdesi %': element.participationPercentageString,
		// };
		var students = [];
		for (let j = 0; j < rowData.length; j++) {
			const element = rowData[j];
			students.push({
				'Öğrenci No': element.studentId,
				'Kimlik No': element.studentIdentityNo,
				'Öğrenci Adı': element.studentName,
				'Planlanan Ders': element.plannedLessonCount,
				'Telafi Ders Sayısı': element.compensationLessonCount,
				'Katıldığı Ders Sayısı': element.joinedLessonCount,
				'Katılmadığı Ders Sayısı': element.notJoinedLessonCount,
				'Katılım Yüzdesi %': element.participationPercentageString,
			});
		}
		// var excelData["Öğrenciler"] = students;
		return students;
	}
}
