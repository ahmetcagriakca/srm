using Fix.Environment.Dependency;
using Fix.Exceptions.Handlers;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Fix.Exceptions.Policy
{
    public class ExceptionPolicy : IExceptionPolicy
    {
        private readonly IPolicyProvider policyProvider;
        private readonly IDependencySolver dependencySolver;

        public ExceptionPolicy(IPolicyProvider policyProvider, IDependencySolver dependencySolver)
        {
            this.policyProvider = policyProvider ?? throw new ArgumentNullException(nameof(policyProvider));
            this.dependencySolver = dependencySolver ?? throw new ArgumentNullException(nameof(dependencySolver));
        }
        public void Execute(Exception exception)
        {
            var policy = policyProvider.Get(exception);
            var handlers = policy.Types.Select(t => dependencySolver.Get(t) as IExceptionHandler);
            policy.Iterator.Iterate(exception, handlers);
        }

        public async Task ExecuteAsync(Exception exception)
        {
            await ExecuteAsync(exception, CancellationToken.None);
        }

        public async Task ExecuteAsync(Exception exception, CancellationToken cancellationToken)
        {
            var policy = policyProvider.Get(exception);
            var handlers = policy.Types.Select(t => dependencySolver.Get(t) as IExceptionHandler);
             await policy.Iterator.IterateAsync(exception, handlers, cancellationToken);
        }
    }
}
