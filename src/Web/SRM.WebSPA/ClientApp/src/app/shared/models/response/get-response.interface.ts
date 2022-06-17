import { IBaseResponse } from "shared/models";

export interface IGetResponseGenerics<T> extends IBaseResponse   {
    entities: T[];
}

export interface IGetResponse extends IGetResponseGenerics<any>   {
}