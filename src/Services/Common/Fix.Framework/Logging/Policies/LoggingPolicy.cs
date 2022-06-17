using Fix.Logging.Iteration;
using System;
using System.Collections.Generic;

namespace Fix.Logging.Policies
{
    public class LoggingPolicy : ILoggingPolicy
    {
        public LoggingPolicy(LogLevel logLevel, ILogIterator logIterator, Func<LogLevel, IEnumerable<ILogger>> loggersFactory)
        {
            Iterator = logIterator;
            LoggersFactory = loggersFactory;
            Level = logLevel;
        }
        public LogLevel Level { get; }
        public ILogIterator Iterator { get; }
        public IEnumerable<ILogger> Loggers
        {
            get
            {
                return LoggersFactory.Invoke(Level);
            }
        }
        protected Func<LogLevel, IEnumerable<ILogger>> LoggersFactory { get; set; }
    }
}
