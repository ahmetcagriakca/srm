import { ParameterBaseModel } from "shared/models/base.model";

export class ApplicationParameter extends ParameterBaseModel {
	name: string;
	description: string;
	value: string;
}
