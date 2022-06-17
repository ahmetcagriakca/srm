using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fix.Logging.Iteration
{
    public class NullIterator : ILogIterator
    {
        public void Iterate<T>(IEnumerable<ILogger> loggers, LogData<T> logData) where T : class
        {

        }

        public Task IterateAsync<T>(IEnumerable<ILogger> loggers, LogData<T> logData) where T : class
        {
            return Task.FromResult(0);
        }
    }
}
