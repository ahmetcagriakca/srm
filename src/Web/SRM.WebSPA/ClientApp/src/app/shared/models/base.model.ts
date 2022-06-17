import { IBaseModel } from "shared/models";

export class BaseModel implements IBaseModel{
    id?;
    createdBy?;
    createdOn?: Date;
    modifiedBy?;
    modifiedOn?: Date;
}
export class ParameterBaseModel extends BaseModel{
    isActive?:boolean;
}
