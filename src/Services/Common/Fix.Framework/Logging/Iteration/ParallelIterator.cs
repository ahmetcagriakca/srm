using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fix.Logging.Iteration
{
    public class ParallelIterator : ILogIterator
    {
        public void Iterate<T>(IEnumerable<ILogger> loggers, LogData<T> logData) where T : class
        {
            Parallel.ForEach(loggers, (logger) =>
            {
                try
                {
                    logger.Log(logData);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Write(ex);
                }
            });
        }

        public async Task IterateAsync<T>(IEnumerable<ILogger> loggers, LogData<T> logData) where T : class
        {
            await Task.Run(() =>
            {
                Parallel.ForEach(loggers, async (logger) =>
                {
                    try
                    {
                        await logger.LogAsync(logData);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.Write(ex);
                    }
                });
            });



            //var tasks = loggers.Select(async logger =>
            //{
            //    try
            //    {
            //        await logger.LogAsync(logData);
            //    }
            //    catch (Exception ex)
            //    {
            //        System.Diagnostics.Debug.Write(ex);
            //    }
            //});

            //await Task.WhenAll(tasks);
        }

    }
}
