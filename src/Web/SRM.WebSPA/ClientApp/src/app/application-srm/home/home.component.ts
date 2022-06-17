import { Component, OnInit } from '@angular/core';
import { DashboardService } from 'shared/services';
import { BasePageComponent } from 'shared/components';
import { MessageService } from 'prime/message/messageservice';

@Component({
	selector: 'home',
	templateUrl: './home.component.html',
})

export class HomeComponent extends BasePageComponent {
	dailyLessonStatus: any;
	comgingStatus: any;
	dailyLessonStatusRange: Date[];
	dailyLessonStatusDetail: any;
	comingStatusDetail: any;
	dailyLessonStatusStyle: any;
	comgingStatusStyle: any;

	constructor(
		public messageService: MessageService,
		public dashboardService: DashboardService
	) {
		super(messageService);

	}

	ngOnInit() {

		var searchDateString = new Date().toISOString();
		var date = new Date();
		this.dailyLessonStatusRange = [
			new Date(date.getFullYear(), date.getMonth(), 1),
			new Date(date.getFullYear(), date.getMonth(), date.getDate()),
		]
		this.searchStatistics();
	}
	searchStatistics() {
		this.searchDailyLessonStatus();
		this.searchComingStatus();
	}

	searchDailyLessonStatus() {
		if (this.dailyLessonStatusRange && this.dailyLessonStatusRange.length > 0) {
			var startDateString = this.dailyLessonStatusRange[0].toISOString();
			if (this.dailyLessonStatusRange.length > 1 && this.dailyLessonStatusRange[1]) {
				var endDateString = this.dailyLessonStatusRange[1].toISOString();
			}
			var request = {
				startDate: startDateString,
				endDate: endDateString
			};
			this.dashboardService.getDailyLessonStatusStatistics(request)
				.subscribe(response => {
					var entity = response.resultValue;
					var plannedLessonPercentage = (entity.plannedLessonCount / entity.totalCount) * 100;
					var compensationLessonPercentage = (entity.compensationLessonCount / entity.totalCount) * 100;
					this.dailyLessonStatusDetail = 'Planlanan (' + entity.plannedLessonCount + '): %' + plannedLessonPercentage.toFixed(2);
					this.dailyLessonStatusDetail += ' ';
					this.dailyLessonStatusDetail += 'Telafi (' + entity.compensationLessonCount + '): %' + compensationLessonPercentage.toFixed(2);
					// this.dailyLessonStatusStyle = "{'text-align': 'center', 'font-weight': 'bold'}";

					this.dailyLessonStatus = {
						labels: ['Planlanan', 'Telafi'],
						datasets: [
							{
								data: [entity.plannedLessonCount, entity.compensationLessonCount],
								backgroundColor: [
									'#3eb839',
									'#f6ac2b'
								]
							}]
					};
				});
		}
		else {

		}
	}

	searchComingStatus() {
		if (this.dailyLessonStatusRange && this.dailyLessonStatusRange.length > 0) {
			var startDateString = this.dailyLessonStatusRange[0].toISOString();
			if (this.dailyLessonStatusRange.length > 1 && this.dailyLessonStatusRange[1]) {
				var endDateString = this.dailyLessonStatusRange[1].toISOString();
			}
			var comingRequest = {
				startDate: startDateString,
				endDate: endDateString
			};
			this.dashboardService.getComingStatusStatistics(comingRequest)
				.subscribe(response => {
					var entity = response.resultValue;
					var comingLessonPercentage = (entity.comingLessonCount / entity.totalCount) * 100;
					var notComingLessonPercentage = (entity.notComingLessonCount / entity.totalCount) * 100;
					this.comingStatusDetail = 'Gelenler (' + entity.comingLessonCount + '): %' + comingLessonPercentage.toFixed(2);
					this.comingStatusDetail += ' ';
					this.comingStatusDetail += 'Gelmeyenler (' + entity.notComingLessonCount + '): %' + notComingLessonPercentage.toFixed(2);
					// this.comgingStatusStyle = "{'text-align': 'center', 'font-weight': 'bold'}";
					this.comgingStatus = {
						labels: ['Gelenler', 'Gelmeyenler'],
						datasets: [
							{
								data: [entity.comingLessonCount, entity.notComingLessonCount],
								backgroundColor: [
									'#3eb839',
									'#f6ac2b'
								]
							}]
					};
				});
		}
		else {

		}
	}
}
