using System;
using System.ComponentModel;

namespace Fix.Mvc
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ServiceResult<T> : ServiceResult
    {
        /// <summary>
        /// 
        /// </summary>
        public T Result { get; set; }

        public ServiceResult(T result, string message = "", ResultState state = ResultState.Successfully) : base(message, state)
        {
            Result = result;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ServiceResult
    {
        /// <summary>
        /// 
        /// </summary>
        public ServiceResult(string message = "", ResultState state = ResultState.Successfully, string[] errors = null, bool isHandledException = false)
        {
            ResponseTime = DateTime.Now;
            State = state;
            Message = message.IsNullOrEmpty() ? ResultState.Successfully.GetDescription<ResultState>() : message;
            Errors = errors;
            IsHandledException = isHandledException;
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime ResponseTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ResultState State { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string[] Errors { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsHandledException { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum ResultState
    {
        /// <summary>
        /// 
        /// </summary>
        [Description("İşlem Başarılı")]
        Successfully = 1,
        /// <summary>
        /// 
        /// </summary>
        [Description("İşlem Sırasında Hata Oluştu")]
        Error,
        /// <summary>
        /// 
        /// </summary>
        Fail,
        /// <summary>
        /// 
        /// </summary>
        Warning,
        /// <summary>
        /// 
        /// </summary>
        Info
    }

}
