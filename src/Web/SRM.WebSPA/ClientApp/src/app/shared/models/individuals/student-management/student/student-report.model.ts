import { BaseModel } from "shared/models/base.model";
import { Hospital } from "shared/models/individuals/parameters/hospital/hospital.model";

export class StudentReport extends BaseModel {
    reportNumber: number;
    givenHospital: Hospital;
    startDate: Date;
    endDate: Date;
    description: string;
    isActive: boolean;
    content: string;
    constructor() {
        super()
        this.givenHospital = new Hospital();
    }
}
