using Fix.Environment.Dependency;
using System;
using System.Collections.Generic;

namespace Fix.Logging
{
    public class LoggerFactory : ILoggerFactory
    {
        private readonly IDependencySolver dependencySolver;

        public LoggerFactory(IDependencySolver dependencySolver)
        {
            this.dependencySolver = dependencySolver ?? throw new ArgumentNullException(nameof(dependencySolver));
        }
        public ILogger GetLogger<T>() where T : ILogger
        {
            return dependencySolver.Get<T>();
        }

        public IEnumerable<ILogger> GetLoggers(LogLevel logLevels)
        {
            throw new NotImplementedException();
        }
    }
}
