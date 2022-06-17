import { BaseModel } from "shared/models/base.model";
import { Page } from "./page.model";
import { Role } from "shared/models/security/accounts/role/role.model";

export class PageRole extends BaseModel {
	pageId?;
	roleId?;
	page: Page;
	role: Role;
}
