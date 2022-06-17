import { BaseModel } from "shared/models/base.model";
import { Hospital } from "shared/models/individuals/parameters/hospital/hospital.model";
import { AvailableTime } from "shared/models/times/available-time.model";
import { Student } from "shared/models/individuals/student-management/student/student.model";

export class StudentAvailableTime extends AvailableTime {
	student: Student;
	constructor() {
		super()
		this.student = new Student();
	}
}
