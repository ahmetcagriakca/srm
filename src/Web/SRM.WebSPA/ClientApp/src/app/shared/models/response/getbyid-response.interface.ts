import { IBaseResponse } from "shared/models";

export interface IGetByIdResponseGenerics<T> extends IBaseResponse {
    entity: T;
}

export interface IGetByIdResponse extends IGetByIdResponseGenerics<any> {
}
