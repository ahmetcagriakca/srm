using System;

namespace SRM.Services.Api.BaseModel
{
    //TODO
    public class BaseServiceResponse
    {
        public BaseServiceResponse()
        {
            ResponseTime = DateTime.Now;
            IsSuccess = true;
        }

        public bool IsSuccess { get; set; }
        public dynamic ResultValue { get; set; }
        public DateTime ResponseTime { get; set; }
    }

    public class Error
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public ErrorType ErrorType { get; set; }
    }

    public enum ErrorType
    {
        Error = 1,
        Warning = 2
    }
}