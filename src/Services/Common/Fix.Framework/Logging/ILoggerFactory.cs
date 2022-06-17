using System.Collections.Generic;

namespace Fix.Logging
{
    public interface ILoggerFactory : ISingleton
    {
        ILogger GetLogger<T>() where T : ILogger;
        IEnumerable<ILogger> GetLoggers(LogLevel logLevels);
    }
}
