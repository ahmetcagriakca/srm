import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MessageService } from 'prime/message/messageservice';
import { ParameterBasePageComponent } from 'shared/components';
import { LocationRegion } from 'shared/models';
import { LocationRegionService } from 'shared/services';
import { SelectItem, ConfirmationService } from 'primeng/primeng';

@Component({
	selector: 'location-region',
	templateUrl: './location-region.component.html'
})

export class LocationRegionComponent extends ParameterBasePageComponent<LocationRegion, LocationRegionService> {
	public SubRegionList: any;
	public SubRegions: SelectItem[];
	public selectedSubRegions: number[];
	constructor(
		public service: LocationRegionService,
		public messageService: MessageService,
		public confirmationService: ConfirmationService,

	) {
		super(LocationRegion, service, messageService, confirmationService);
	}

	ngOnInit() {
		this.baseForm = new FormGroup({
			'id': new FormControl(''),
			'name': new FormControl('', Validators.required),
			'code': new FormControl('', [ Validators.required, Validators.maxLength(5) ]),
			'isActive': new FormControl('', Validators.required),
			'subRegions': new FormControl(''),
		});
		this.searchForm = new FormGroup({
			'id': new FormControl(''),
			'name': new FormControl(''),
			'code': new FormControl(''),
			'isActive': new FormControl(null),
			'subRegion': new FormControl(''),
		});

		this.SubRegions = [];
		this.service.get()
			.subscribe(response => {
				var entities = (<LocationRegion[]>response.resultValue);
				for (let entity of entities) {
					this.SubRegions.push({ label: entity.name, value: entity.id });
				}
			});

		this.SubRegionList = [{ label: "Seçiniz", value: null }];
		this.service.get()
			.subscribe(response => {
				var entities = (<LocationRegion[]>response.resultValue);
				for (let entity of entities) {
					this.SubRegionList.push({ label: entity.name, value: entity.id });
				}
			});
		super.ngOnInit();
	}
	
	initAdd(record: LocationRegion) {
		this.selectedSubRegions = [];
	}

	initEdit(record: LocationRegion) {
		this.selectedSubRegions = [];
		if (record.subRegions != null && record.subRegions.length > 0) {
			record.subRegions.forEach(element => {
				this.selectedSubRegions.push(element.id);
			});
		}
	}
	beforeSave(record: any) {
		record.SubRegionIds = this.selectedSubRegions;
		// record.subRegions.forEach(element => {
		// 	record.SubRegionIds.push(element.id);
		// });

	}
}
