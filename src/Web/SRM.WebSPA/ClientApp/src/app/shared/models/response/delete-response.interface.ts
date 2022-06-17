import { IBaseResponse } from "shared/models";

export interface IDeleteResponseGenerics<T > extends IBaseResponse   {
}

export interface IDeleteResponse extends IDeleteResponseGenerics<any> {
}
