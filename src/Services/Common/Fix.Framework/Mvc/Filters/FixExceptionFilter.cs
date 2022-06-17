using Fix.Data;
using Fix.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace Fix.Mvc.Filters
{
    public class FixExceptionFilter : ExceptionFilterAttribute
    {
        private readonly IExceptionManager exceptionManager;
        private readonly IActionResultBuilder jsonResultService;
        private readonly ITransactionManager transactionManager;
        public FixExceptionFilter(
            IExceptionManager exceptionManager,
            IActionResultBuilder jsonResultService,
            ITransactionManager transactionManager
            )
        {
            this.exceptionManager = exceptionManager ?? throw new ArgumentNullException(nameof(exceptionManager));
            this.jsonResultService = jsonResultService ?? throw new ArgumentNullException(nameof(jsonResultService));
            this.transactionManager = transactionManager ?? throw new ArgumentNullException(nameof(transactionManager));
        }
        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            await transactionManager.RollbackAsync();
            await exceptionManager.HandleAsync(context.Exception);
            context.Result = jsonResultService.Build(context.Exception);
        }
    }
}
