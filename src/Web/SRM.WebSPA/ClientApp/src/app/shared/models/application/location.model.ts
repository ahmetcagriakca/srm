import { BaseModel } from "shared/models/base.model";
import { LocationRegion } from "shared/models/shuttles/parameters/location-region.model";

export class Location extends BaseModel {
	latitude: number;
	longitude: number;
	
    constructor() {
        super()
      
    }
}
