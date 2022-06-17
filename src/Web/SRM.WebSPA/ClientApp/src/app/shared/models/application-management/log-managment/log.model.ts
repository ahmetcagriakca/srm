import { BaseModel } from "shared/models/base.model";

export class Log extends BaseModel {
	message?;
	channel?;
	level?;//1-warning
	url?;
	stack?;
}