import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MessageService } from 'prime/message/messageservice';
import { ParameterBasePageComponent } from 'shared/components';
import { StudentService, User } from 'shared/models';
import { StudentServiceService, AccountService } from 'shared/services';
import { SelectItem, ConfirmationService } from 'primeng/primeng';

@Component({
	selector: 'student-service',
	templateUrl: './student-service.component.html'
})

export class StudentServiceComponent extends ParameterBasePageComponent<StudentService, StudentServiceService> {

	public Drivers: SelectItem[];
	public selectedDriver: number;

	constructor(
		public service: StudentServiceService,
		public accountService: AccountService,
		public messageService: MessageService,
		public confirmationService: ConfirmationService,

	) {
		super(StudentService, service, messageService, confirmationService);
	}

	ngOnInit() {
		this.baseForm = new FormGroup({
			'id': new FormControl(''),
			'plate': new FormControl('', Validators.required),
			'maxCapacity': new FormControl('', Validators.required),
			'driver': new FormControl('', Validators.required),
			'isActive': new FormControl('', Validators.required),
		});
		this.searchForm = new FormGroup({
			'id': new FormControl(''),
			'plate': new FormControl(''),
			'maxCapacity': new FormControl(''),
			'driver': new FormControl(''),
			'isActive': new FormControl(null),
		});
		super.ngOnInit();

		this.Drivers = [{ label: "Seçiniz", value: null }];
		this.accountService.getDrivers()
			.subscribe(response => {
				var entities = (<User[]>response.resultValue);
				for (let entity of entities) {
					this.Drivers.push({ label: (entity.username ? entity.username : '') + "-" + (entity.name ? entity.name : '') + " " + (entity.name ? entity.surname : ''), value: entity.id });
				}
			});
	}
	initAdd(record: StudentService) {
		this.selectedDriver = null;
	}

	initEdit(record: StudentService) {
		if (this.record.driver)
			this.selectedDriver = this.record.driver.id;
		else {
			this.selectedDriver = null;
		}
	}
	beforeSave(record: any) {
		record.driver = this.selectedDriver;
		// record.subRegions.forEach(element => {
		// 	record.SubRegionIds.push(element.id);
		// });

	}
}
