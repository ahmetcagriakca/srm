using Fix.Data;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Fix.Mvc.Filters
{
    public class TransactionFilter : IResultFilter
    {
        private readonly ITransactionManager transactionManager;

        public TransactionFilter(ITransactionManager transactionManager)
        {
            this.transactionManager = transactionManager;
        }
        public void OnResultExecuted(ResultExecutedContext context)
        {
            transactionManager.Commit();
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {

        }
    }
}
