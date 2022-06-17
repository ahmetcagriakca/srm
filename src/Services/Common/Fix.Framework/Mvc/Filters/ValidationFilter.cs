using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Fix.Mvc.Filters
{
    public class ValidationFilter : IActionFilter
    {
        private readonly IActionResultBuilder resultBuilder;
        public ValidationFilter(IActionResultBuilder resultBuilder)
        {
            this.resultBuilder = resultBuilder ?? throw new ArgumentNullException(nameof(resultBuilder));
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //TODO: Service result checked and status code will modify((ServiceResult) ((ObjectResult) context.Result).Value)//    .State;
            //context.Result = resultBuilder.Build(context.Rz);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid && context.ActionArguments.All(kv => kv.Value != null))
            {
                return;
            }
            context.Result = resultBuilder.Build(context.ModelState);
        }
    }
}
