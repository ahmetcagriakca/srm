using Fix.Exceptions.Policy;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Fix.Exceptions
{
    public class ExceptionManager : IExceptionManager
    {
        private readonly IExceptionPolicy exceptionPolicy;

        public ExceptionManager(IExceptionPolicy exceptionPolicy)
        {
            this.exceptionPolicy = exceptionPolicy ?? throw new ArgumentNullException(nameof(exceptionPolicy));
        }
        public void Handle(Exception exception)
        {
            exceptionPolicy.Execute(exception);
        }

        public async Task HandleAsync(Exception exception)
        {
            await HandleAsync(exception, CancellationToken.None);
        }

        public async Task HandleAsync(Exception exception, CancellationToken cancellationToken)
        {
            await exceptionPolicy.ExecuteAsync(exception);
        }
    }
}
