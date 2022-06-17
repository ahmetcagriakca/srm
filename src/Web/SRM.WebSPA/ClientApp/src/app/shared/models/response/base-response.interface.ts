export interface IBaseResponse extends Response   {
    isSuccess: boolean;
    resultValue?: any;
    errors?;
    error?;
}
