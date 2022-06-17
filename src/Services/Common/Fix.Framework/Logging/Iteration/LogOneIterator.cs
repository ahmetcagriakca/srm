using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fix.Logging.Iteration
{
    public class LogOneIterator : ILogIterator
    {
        public void Iterate<T>(IEnumerable<ILogger> loggers, LogData<T> logData) where T : class
        {
            foreach (var logger in loggers)
            {
                try
                {
                    logger.Log(logData);
                    break;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Write(ex);
                }
            }
        }

        public async Task IterateAsync<T>(IEnumerable<ILogger> loggers, LogData<T> logData) where T : class
        {
            foreach (var logger in loggers)
            {
                try
                {
                    await logger.LogAsync(logData);
                    break;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Write(ex);
                }
            }
        }
    }
}
