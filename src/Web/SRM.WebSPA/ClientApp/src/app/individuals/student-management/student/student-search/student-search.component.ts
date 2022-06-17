import { Component } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { MessageService } from 'prime/message/messageservice';
import { StudentService, ObstacleTypeService, LocationRegionService } from 'shared/services';
import { BasePageComponentGenerics } from 'shared/components';
import { StudentSearch, ObstacleType, LocationRegion } from 'shared/models';

@Component({
    selector: 'student-search',
    templateUrl: './student-search.component.html'
})

export class StudentSearchComponent extends BasePageComponentGenerics<StudentSearch, StudentService> {
    public ObstacleTypes: any;
    public LocationRegions: any;
    constructor(public service: StudentService,
        public messageService: MessageService,
        public obstacleTypeService: ObstacleTypeService,
        public locationRegionService: LocationRegionService) {
        super(StudentSearch, service, messageService);
    }


    ngOnInit() {
        this.baseForm = new FormGroup({
            'id': new FormControl(''),
            'identityNumber': new FormControl(''),
            'name': new FormControl(''),
            'surname': new FormControl(''),
            'obstacleType': new FormControl(''),
            'reportStartDate': new FormControl(''),
            'reportEndDate': new FormControl(''),
            'isActive': new FormControl(''),
            'locationRegion': new FormControl('')
        });
        this.baseForm.patchValue({
            'isActive': true,
            // formControlName2: myValue2 (can be omitted)
        });
        super.ngOnInit();
        this.ObstacleTypes = [{ label: "Seçiniz", value: null }];
        this.LocationRegions = [{ label: "Seçiniz", value: null }];
        this.obstacleTypeService.get()
            .subscribe(response => {
                var entities = (<ObstacleType[]>response.resultValue);
                for (let entity of entities) {
                    this.ObstacleTypes.push({ label: entity.name, value: entity.id });
                }

                this.locationRegionService.get()
                    .subscribe(response => {
                        var entities = (<LocationRegion[]>response.resultValue);
                        for (let entity of entities) {
                            this.LocationRegions.push({ label: entity.name, value: entity.id });
                        }
                        this.search();
                    });
            });
    }
    search(formData?: any) {
        if (formData && formData.reportStartDate) {
            formData.reportStartDate = formData.reportStartDate != null ? formData.reportStartDate.toISOString() : null;
            formData.reportEndDate = formData.reportEndDate != null ? formData.reportEndDate.toISOString() : null;
		}
        this.service.searchStudents(formData)
            .subscribe(response => {
                if (response.isSuccess == true) {
                    this.rowData = (<StudentSearch[]>response.resultValue);
                }
            });

    }

    cleanData() {
        super.cleanData()
    }

    falseStateName: string = "Pasif";
    trueStateName: string = "Aktif";
    nullStateName: string = "Her ikisi de";
    triStatelabel: string = this.nullStateName;
    setStateLabel(state?: boolean) {
        switch (state) {
            case true: this.triStatelabel = this.trueStateName;
                break;
            case false: this.triStatelabel = this.falseStateName;
                break;
            case null: this.triStatelabel = this.nullStateName;
                break;
        }
    }

    onTriStateChange(event) {
        this.setStateLabel(event.value);
    }
}
