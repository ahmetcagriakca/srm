import { BaseModel } from "shared/models/base.model";
import { LocationRegion } from "shared/models/shuttles/parameters/location-region.model";
import { StudentService } from "shared/models/shuttles/parameters/student-service.model";
import { Student } from "shared/models/individuals/student-management/student/student.model";

export class ShuttleTemplate extends BaseModel {
    locationRegion: LocationRegion;
    studentService: StudentService;
    dayOfWeek: number;
	hourOfDate: number;
	students: Student[];
	/**
	 *
	 */
	constructor() {
		super();
		this.locationRegion= new LocationRegion();
		this.studentService= new StudentService();
	}
}