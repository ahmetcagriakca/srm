using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fix.Logging.Iteration
{
    public interface ILogIterator
    {
        void Iterate<T>(IEnumerable<ILogger> loggers, LogData<T> logData) where T : class;
        Task IterateAsync<T>(IEnumerable<ILogger> loggers, LogData<T> logData) where T : class;
    }
}
