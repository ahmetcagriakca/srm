import { BaseModel } from "shared/models/base.model";

export class Individual extends BaseModel {
    identityNumber:string;
    name:string;
    surname:string;
}
