import { BaseModel } from "shared/models/base.model";
import { ShuttleTemplate } from "shared/models/shuttles/template-management/shuttle-template.model";

export class ShuttleOperation extends BaseModel {
    shuttleTemplate: ShuttleTemplate;
    dateTime: Date;
}
