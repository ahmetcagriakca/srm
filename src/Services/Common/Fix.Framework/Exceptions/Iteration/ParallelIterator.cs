using Fix.Exceptions.Handlers;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Fix.Exceptions.Iteration
{
    public class ParallelIterator : IExceptionIterator
    {
        public void Iterate(Exception exception, IEnumerable<IExceptionHandler> exceptionHandlers)
        {
            Parallel.ForEach(exceptionHandlers, (handler) =>
            {
                try
                {
                    handler.Handle(exception);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Write(ex);
                }
            });
        }


        public async Task IterateAsync(Exception exception, IEnumerable<IExceptionHandler> exceptionHandlers)
        {
            await IterateAsync(exception, exceptionHandlers, CancellationToken.None);
        }

        public async Task IterateAsync(Exception exception, IEnumerable<IExceptionHandler> exceptionHandlers, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                Parallel.ForEach(exceptionHandlers, async (handler) =>
                {
                    try
                    {
                        await handler.HandleAsync(exception);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.Write(ex);
                    }
                });
            });
        }
    }
}
