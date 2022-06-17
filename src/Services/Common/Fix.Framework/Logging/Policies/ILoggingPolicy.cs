using Fix.Logging.Iteration;
using System.Collections.Generic;

namespace Fix.Logging.Policies
{
    public interface ILoggingPolicy
    {
        LogLevel Level { get; }
        ILogIterator Iterator { get; }
        IEnumerable<ILogger> Loggers { get; }
    }
}
