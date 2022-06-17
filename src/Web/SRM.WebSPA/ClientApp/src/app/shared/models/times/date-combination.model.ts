import { BaseModel } from "shared/models/base.model";

export class DateCombination extends BaseModel {
	monday: boolean;
	tuesday: boolean;
	wednesday: boolean;
	thursday: boolean;
	friday: boolean;
	saturday: boolean;
	sunday: boolean;
}