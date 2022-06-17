import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MessageService } from 'prime/message/messageservice';
import { ActivatedRoute } from '@angular/router';
import { Subject } from 'rxjs';
import { StudentService, ObstacleTypeService, LocationRegionService } from 'shared/services';
import { BasePageComponent } from 'shared/components';
import { Student, ObstacleType, LocationRegion } from 'shared/models';
import { SelectItem, ConfirmationService } from 'primeng/primeng';
import { LoaderService } from 'shared/components/loader/loader.service';

@Component({
	selector: 'student',
	templateUrl: './student.component.html'
})

export class StudentComponent extends BasePageComponent {
	id: number;
	record: Student;
	public ObstacleTypes: SelectItem[];
	public selectedObstacleTypes: number[];
	public checkSave: boolean = false;

	constructor(public service: StudentService,
		private route: ActivatedRoute,
		public messageService: MessageService,
		private obstacleTypeService: ObstacleTypeService,
		private locationRegionService: LocationRegionService,
		private confirmationService: ConfirmationService,
	) {
		super(messageService);
	}

	ngOnInit() {
		super.ngOnInit();
		this.baseForm = new FormGroup({
			'id': new FormControl(''),
			'identityNumber': new FormControl('', Validators.required),
			'name': new FormControl('', Validators.required),
			'surname': new FormControl('', Validators.required),
			'dateOfBirth': new FormControl('', Validators.required),
			// 'parentName': new FormControl('', Validators.required),
			// 'parentPhoneNumber': new FormControl('', Validators.required),
			'obstacleTypes': new FormControl(''),
			'isActive': new FormControl('', Validators.required),
		});

		const id =
			this.route.queryParams.subscribe(params => {
				console.log(params);
			});

		this.ObstacleTypes = [];
		this.obstacleTypeService.get()
			.subscribe(response => {
				var entities = (<ObstacleType[]>response.resultValue);
				for (let entity of entities) {
					this.ObstacleTypes.push({ label: entity.name, value: entity.id });
				}
			});
		this.loadData();
	}

	clean() {
		this.service.studentCleanedSource.next(this.id);
		this.service.changeClean(true);
		this.record = new Student();
		this.record.isActive = true;
		this.selectedObstacleTypes = [];
		this.baseForm.controls["identityNumber"].enable();
	}

	setStudentInfo(record: Student) {
		this.record = record;
		this.id = this.record.id;
		this.service.changeClean(false);
		if (this.record.dateOfBirth) {
			this.record.dateOfBirth = new Date(this.record.dateOfBirth);
		}
		this.selectedObstacleTypes = []
		for (let obstacleType of this.record.obstacleTypes) {
			this.selectedObstacleTypes.push(obstacleType.id);
		}
		this.disableFields();
	}

	loadData() {
		this.clean();
		if (!this.id) {
			this.id = Number(this.route.snapshot.paramMap.get('id'));
		}

		if (this.id && this.id != 0) {
			this.checkSave = true;
			this.service.getStudentById(this.id)
				.subscribe(response => {
					this.service.studentLoadedSource.next(this.id);
					this.setStudentInfo(<Student>response.resultValue);
					this.baseForm.controls["identityNumber"].disable();
				});
		}
		else {
			this.checkSave = false;
			this.disableFields();
		}
	}

	disableFields() {
		this.baseForm.controls["id"].disable();
	}

	save() {
		if (super.isValid()) {
			if (this.id) {
				this.baseForm.controls["identityNumber"].enable();
				this.service.updateStudent(this.id, this.baseForm.value)
					.subscribe(response => {
						if (response.isSuccess) {
							this.messageService.add({ severity: 'success', summary: 'İşlem Başarılı', detail: 'Kayıt Başarıyla Güncellendi.' });
							this.loadData();
						} else {
							this.showErrors(response);
						}
					});

				this.baseForm.controls["identityNumber"].disable();
			} else {
				this.checkSave = true;
				this.service.createStudent(this.baseForm.value)
					.subscribe(response => {
						if (response.isSuccess) {
							this.messageService.add({ severity: 'success', summary: 'İşlem Başarılı', detail: 'Kayıt Başarıyla Eklendi.' });
							this.service.GetStudentByIdentityNumber(this.baseForm.value.identityNumber)
								.subscribe(response => {
									if (response.isSuccess) {

										this.setStudentInfo(<Student>response.resultValue);
									} else {
										this.showErrors(response);
									}
								});
						} else {
							this.showErrors(response);
						}
					});
			}

		}
		else {
			this.messageService.add({ severity: 'error', summary: 'Zorunlu alanları doldurunuz' });

		}
	}
}
