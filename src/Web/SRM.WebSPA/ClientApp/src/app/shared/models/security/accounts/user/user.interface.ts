import { IBaseModel, IRole } from "shared/models";

export interface IUser extends IBaseModel {
    userName?;
    name?;
    surname?;
    email?;
    mobilePhone?;
    userRoleIds?;
    userRoles?: IRole[];
}
