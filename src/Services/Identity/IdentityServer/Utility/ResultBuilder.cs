using Fix.Exceptions;
using Fix.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;

namespace IdentityServer.Utility
{
    /// <summary>
    /// 
    /// </summary>
    public class ResultBuilder : IActionResultBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ActionResult<T> Build<T>(T entity)
        {
            var obj = new ServiceResult<T>(entity, state: ResultState.Error);
            return GetResult(obj, StatusCodes.Status200OK);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public IActionResult Build(ModelStateDictionary dictionary)
        {
            var errors = dictionary
                .Keys
                .SelectMany(i => dictionary[i].Errors)
                .Select(m => m.ErrorMessage)
                .ToArray();
            var obj = new ServiceResult(state: ResultState.Error, errors: errors);
            return GetResult(obj, StatusCodes.Status400BadRequest);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public IActionResult Build(Exception exception)
        {
            return Build(exception, StatusCodes.Status400BadRequest);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        public IActionResult Build(Exception exception, int statusCode)
        {
            bool handledException = exception is FixException;
            var obj = new ServiceResult(exception.Message, state: ResultState.Error, isHandledException: handledException);
            return GetResult(obj, statusCode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="error"></param>
        /// <param name="description"></param>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        public IActionResult Build(string error, string description, int statusCode)
        {
            var obj = new ServiceResult(error, ResultState.Error);
            return GetResult(obj, statusCode);

        }

        /// <summary>
        /// Return json result for actionresult
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        public JsonResult GetResult(object obj, int statusCode = StatusCodes.Status400BadRequest)
        {
            return new JsonResult(obj)
            {
                StatusCode = statusCode
            };
        }
    }
}
