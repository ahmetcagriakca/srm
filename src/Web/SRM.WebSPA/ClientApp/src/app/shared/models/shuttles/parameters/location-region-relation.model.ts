import { BaseModel } from "shared/models/base.model";
import { LocationRegion } from "shared/models/shuttles/parameters/location-region.model";

export class LocationRegionRelation extends BaseModel {
    mainRegion: LocationRegion;
    subRegion: LocationRegion;
}
