import { Component, Input } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MessageService } from 'prime/message/messageservice';
import { ShuttleService, StudentService } from 'shared/services';
import { ConfirmationService } from 'primeng/primeng';
import { ShuttleListComponent } from 'shuttles/operation-management/shuttle-list.component';
import { ShuttleStudentAdviceComponent } from 'shuttles/operation-management/shuttle-student-advice.component';

@Component({
	selector: 'shuttle-student-advice-by-id',
	templateUrl: './shuttle-student-advice.component.html'
})

export class ShuttleStudentAdviceByIdComponent extends ShuttleStudentAdviceComponent {
	shuttleOperationId: any;
	constructor(
		public shuttleListComponent: ShuttleListComponent,
		public service: ShuttleService,
		public messageService: MessageService,
		public confirmationService: ConfirmationService,
		public studentService: StudentService,
	) {
		super(service, messageService, confirmationService, studentService);
	}

	ngOnInit() {
		this.showDateSearch = false;
		this.service.shuttleAdviceByIdLoad$.subscribe(shuttleOperationId => {
			this.shuttleOperationId = shuttleOperationId;
			this.loadData(false);
		});
		super.ngOnInit();
	}

	loadData(isAfterLoad: boolean) {
		if (isAfterLoad == true) {
			this.shuttleListComponent.loadData();
		}
		this.getStudentShuttleCallListById(this.shuttleOperationId);
	}

	getStudentShuttleCallListById(id: number) {
		if (id) {
			this.service.getStudentOperationAdvicesByShuttleOperationId(id)
				.subscribe(response => {
					this.setInfo(response.resultValue);
				});
		}
	}
}
