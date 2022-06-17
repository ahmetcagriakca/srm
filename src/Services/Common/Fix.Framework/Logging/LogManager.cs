using System;

namespace Fix.Logging
{
    public class LogManager : ILogManager
    {
        private readonly ILoggerFactory loggerFactory;

        public LogManager(ILoggerFactory loggerFactory, IChainLogger chainLogger)
        {
            this.loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
            Logger = chainLogger ?? throw new ArgumentNullException(nameof(chainLogger));
        }

        public IChainLogger Logger { get; }

        public ILogger GetLogger<T>() where T : class, ILogger
        {
            return loggerFactory.GetLogger<T>();
        }
    }
}
