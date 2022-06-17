import { BaseModel } from "shared/models/base.model";
import { DateCombination } from "shared/models/times/date-combination.model";

export abstract class AvailableTime extends BaseModel {
	isAvaible: boolean;
	isIntegrated: boolean;
	startDate: Date;
	endDate: Date;
	startTime: Date;
	endTime: Date;
	description: number;
	includedDate: DateCombination;
	constructor(){
		super()
		this.includedDate=new DateCombination();
	}
}