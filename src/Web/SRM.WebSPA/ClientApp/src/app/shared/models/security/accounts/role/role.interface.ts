import { IBaseModel } from "shared/models";

export interface IRole extends IBaseModel {
    name?;
    description?;
    order?;
}
