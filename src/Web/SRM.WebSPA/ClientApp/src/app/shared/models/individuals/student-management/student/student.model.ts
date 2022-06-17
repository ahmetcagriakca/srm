import { Individual } from "shared/models/individuals/individual.model";
import { ObstacleType } from "shared/models/individuals/parameters/obstacle-type/obstacle-type.model";
import { Lesson } from "shared/models/courses/lesson/lesson.model";
import { LocationRegion } from "shared/models/shuttles/parameters/location-region.model";

export class Student extends Individual {
	dateOfBirth: Date;
	parentName: string;
	parentPhoneNumber: string;
	obstacleTypes: ObstacleType[];
	courseStartDate: Date;
	isActive: boolean;
	address: string;
	lessons: Lesson[];
	locationRegion: LocationRegion;
	constructor() {
		super()
		this.obstacleTypes = [];
	}
}
