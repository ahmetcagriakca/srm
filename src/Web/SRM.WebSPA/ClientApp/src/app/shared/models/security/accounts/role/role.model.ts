import { BaseModel } from "shared/models/base.model";

export class Role extends BaseModel {
    name?;
    description?;
	order?;
	isActive?;
}
