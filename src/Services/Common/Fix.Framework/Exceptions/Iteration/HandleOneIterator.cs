using Fix.Exceptions.Handlers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Fix.Exceptions.Iteration
{
    public class HandleOneIterator : IExceptionIterator
    {
        public void Iterate(Exception exception, IEnumerable<IExceptionHandler> exceptionHandlers)
        {
            foreach (var handler in exceptionHandlers)
            {
                try
                {
                    handler.Handle(exception);
                    break;
                }
                catch (Exception ex)
                {
                    Debug.Write(ex);
                }
            }
        }

        public async Task IterateAsync(Exception exception, IEnumerable<IExceptionHandler> exceptionHandlers)
        {
            await IterateAsync(exception, exceptionHandlers, CancellationToken.None);
        }

        public async Task IterateAsync(Exception exception, IEnumerable<IExceptionHandler> exceptionHandlers, CancellationToken cancellationToken)
        {
            foreach (var handler in exceptionHandlers)
            {
                try
                {
                    if (!cancellationToken.IsCancellationRequested)
                    {
                        await handler.HandleAsync(exception);
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Debug.Write(ex);
                }
            }
        }
    }
}
