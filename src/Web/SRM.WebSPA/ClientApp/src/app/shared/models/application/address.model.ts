import { BaseModel } from "shared/models/base.model";
import { LocationRegion } from "shared/models/shuttles/parameters/location-region.model";
import {Location} from "./location.model";

export class Address extends BaseModel {
	title: string;
	addressInfo: string;
	addressDirections: string;
	priority: number;
	locationRegion: LocationRegion;
	location:Location;
    constructor() {
        super()
		this.locationRegion = new LocationRegion();
		this.location=new Location();
    }
}
