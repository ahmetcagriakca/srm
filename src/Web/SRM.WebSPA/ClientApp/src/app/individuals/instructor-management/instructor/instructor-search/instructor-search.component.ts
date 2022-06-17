import { Component } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { MessageService } from 'prime/message/messageservice';
import { InstructorService, BranchService } from 'shared/services';
import { BasePageComponentGenerics } from 'shared/components';
import { InstructorSearch, Branch } from 'shared/models';

@Component({
    selector: 'instructor-search',
    templateUrl: './instructor-search.component.html'
})

export class InstructorSearchComponent extends BasePageComponentGenerics<InstructorSearch, InstructorService> {
    public Branches: any;
    constructor(public service: InstructorService, public messageService: MessageService, public branchService: BranchService) {
        super(InstructorSearch, service, messageService);
    }

    ngOnInit() {
        this.baseForm = new FormGroup({
            'id': new FormControl(''),
            'identityNumber': new FormControl(''),
            'name': new FormControl(''),
            'surname': new FormControl(''),
            // 'branch': new FormControl(''),
            'isActive': new FormControl('')
        });
        super.ngOnInit();
        // this.Branches = [{ label: "Seçiniz", value: null }];
        // this.branchService.get()
        //     .subscribe(response => {
        //         var entities = (<Branch[]>response.resultValue);
        //         for (let entity of entities) {
        //             this.Branches.push({ label: entity.name, value: entity.id });
        //         }
        //     });
        this.search();
    }
    search(formData?: any) {
        this.service.search(formData)
            .subscribe(response => {
                if (response.isSuccess == true) {
                    this.rowData = (<InstructorSearch[]>response.resultValue);
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
