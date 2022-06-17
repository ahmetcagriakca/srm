import { IBaseModel } from "shared/models/base.interface";

export interface IIndividual extends IBaseModel {
    identityNumber:string;
    name:string;
    surname:string;
}
