import { ParameterBaseModel } from "shared/models/base.model";
import { User } from "shared/models/security/accounts/user/user.model";

export class StudentService extends ParameterBaseModel {
	plate: string;
	maxCapacity: boolean;
	driver: User;
	
	constructor() {
		super()
		this.driver = new User();
	}
}
