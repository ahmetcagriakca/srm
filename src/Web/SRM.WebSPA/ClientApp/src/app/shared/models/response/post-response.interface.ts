import { IBaseModel, IBaseResponse } from "shared/models";

export interface IPostResponseGenerics<T extends IBaseModel> extends IBaseResponse   {
}
export interface IPostResponse  extends IPostResponseGenerics<any>   {
}
