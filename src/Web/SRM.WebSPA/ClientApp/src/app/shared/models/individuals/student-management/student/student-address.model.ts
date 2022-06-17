import { BaseModel } from "shared/models/base.model";
import { Hospital } from "shared/models/individuals/parameters/hospital/hospital.model";
import { Address } from "shared/models/application/address.model";
import { Student } from "shared/models/individuals/student-management/student/student.model";

export class StudentAddress extends BaseModel {
    address: Address;
    student: Student;
    constructor() {
        super()
        this.address = new Address();
        this.student = new Student();
    }
}
