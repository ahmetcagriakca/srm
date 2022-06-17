import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MessageService } from 'prime/message/messageservice';
import { ActivatedRoute } from '@angular/router';
import { Subject } from 'rxjs';
import { InstructorService, BranchService } from 'shared/services';
import { BasePageComponent } from 'shared/components';
import { Branch, Instructor } from 'shared/models';
import { SelectItem } from 'primeng/primeng';

@Component({
	selector: 'instructor',
	templateUrl: './instructor.component.html'
})

export class InstructorComponent extends BasePageComponent {
	id: number;
	record: Instructor;
	public Branches: SelectItem[];
	public selectedBranches: number[];

	private instructorLoadedSource = new Subject();
	instructorLoaded$ = this.instructorLoadedSource.asObservable();

	constructor(public service: InstructorService,
		private route: ActivatedRoute,
		public messageService: MessageService,
		private branchService: BranchService) {
		super(messageService);
	}

	ngOnInit() {
		super.ngOnInit();
		this.baseForm = new FormGroup({
			'id': new FormControl(''),
			'identityNumber': new FormControl('', Validators.required),
			'name': new FormControl('', Validators.required),
			'surname': new FormControl('', Validators.required),
			// 'branches': new FormControl(''),
			// 'address': new FormControl(''),
			'phone': new FormControl(''),
			'email': new FormControl(''),
			'isActive': new FormControl('', Validators.required)
		});
		this.baseForm.controls['isActive'].setValue(true);

		const id =
			this.route.queryParams.subscribe(params => {
				console.log(params);
			});
		this.loadData();
	}

	setInfo(record: Instructor) {
		this.record = record;
		this.id = this.record.id;
		this.service.changeClean(false);
		if (this.record.hireDate) {
			this.record.hireDate = new Date(this.record.hireDate);
		}
		this.selectedBranches = []
		for (let branch of this.record.branches) {
			this.selectedBranches.push(branch.id);
		}
		this.service.instructorLoadedSource.next(this.id);
		this.disableFields();
	}

	loadData() {
		this.clean();
		this.Branches = [];
		this.branchService.get()
			.subscribe(response => {
				var entities = (<Branch[]>response.resultValue);
				for (let entity of entities) {
					this.Branches.push({ label: entity.name, value: entity.id });
				}
			});
		this.record = new Instructor();
		this.record.isActive = true;
		if (!this.id)
			this.id = Number(this.route.snapshot.paramMap.get('id'));
		if (this.id && this.id != 0) {
			this.service.getById(this.id)
				.subscribe(response => {
					this.setInfo(<Instructor>response.resultValue);
				});
		}
		else {
			this.disableFields();
		}
	}

	disableFields() {
		this.baseForm.controls["id"].disable();
	}


	clean() {
		this.service.instructorCleanedSource.next(this.id);
		this.service.changeClean(true);
		this.record = new Instructor();
		this.record.isActive = true;
		this.baseForm.controls["identityNumber"].enable();
	}
	save() {
		if (super.isValid()) {
			if (this.id) {
				this.baseForm.controls["identityNumber"].enable();
				this.service.put(this.id, this.baseForm.value)
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
				this.service.post(this.baseForm.value)
					.subscribe(response => {
						if (response.isSuccess) {
							this.messageService.add({ severity: 'success', summary: 'İşlem Başarılı', detail: 'Kayıt Başarıyla Eklendi.' });
							this.service.getByIdentityNumber(this.baseForm.value.identityNumber)
								.subscribe(response => {
									if (response.isSuccess) {
										this.setInfo(<Instructor>response.resultValue);
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
