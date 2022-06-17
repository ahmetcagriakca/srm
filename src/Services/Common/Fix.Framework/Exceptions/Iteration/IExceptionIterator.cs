using Fix.Exceptions.Handlers;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace Fix.Exceptions.Iteration
{
    public interface IExceptionIterator
    {
        void Iterate(Exception exception, IEnumerable<IExceptionHandler> exceptionHandlers);
        Task IterateAsync(Exception exception, IEnumerable<IExceptionHandler> exceptionHandlers);
        Task IterateAsync(Exception exception, IEnumerable<IExceptionHandler> exceptionHandlers, CancellationToken cancellationToken);
    }
}
