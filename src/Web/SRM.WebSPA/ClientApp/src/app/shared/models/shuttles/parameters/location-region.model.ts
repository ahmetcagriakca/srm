import { ParameterBaseModel } from "shared/models/base.model";

export class LocationRegion extends ParameterBaseModel {
    name: string;
    code: number;
	subRegions: LocationRegion[]
}
