import { BaseModel } from "shared/models/base.model";
import { Role } from "shared/models/security/accounts/role/role.model";

export class User extends BaseModel {
	username?;
	name?;
	surname?;
	email?;
	mobilePhone?;
	userRoleIds?;
	userRoles?: Role[];

	constructor() {
		super()
		this.userRoles = [];
	}
}
