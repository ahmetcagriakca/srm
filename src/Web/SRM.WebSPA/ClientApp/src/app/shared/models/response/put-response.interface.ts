import { IBaseResponse, IBaseModel } from "shared/models";

export interface IPutResponseGenerics<T extends IBaseModel> extends IBaseResponse   {
}

export interface IPutResponse extends IPutResponseGenerics<any>    {
}