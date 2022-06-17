using System;

namespace Fix.Logging
{
    public class LogDataFactory : ILogDataFactory
    {
        private readonly IWorkContext workContext;

        public LogDataFactory(IWorkContext workContext)
        {
            this.workContext = workContext ?? throw new ArgumentNullException(nameof(workContext));
        }


        public LogData<T> Create<T>(LogLevel logLevels, T instance, string message) where T : class
        {
            var data = new LogData<T>();
            data.CorrlId = workContext.CorrelationId.ToString();
            data.MachineName = System.Environment.MachineName;
            data.TLogInstance = instance;
            data.LogLevel = logLevels;
            data.UtcTimestamp = DateTime.UtcNow;
            data.Message = message;
            data.User = workContext.AuthenticationProvider.GetUserName() ?? "none";


            return data;
        }
    }
}
