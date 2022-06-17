using System.Threading.Tasks;

namespace Fix.Logging.Loggers
{
    public class ConsoleLogger : BaseLogger, ILogger
    {
        public ConsoleLogger(ILogDataFactory logDataFactory) : base(logDataFactory)
        {

        }
        public override string AliasName => "ConsoleLogger";

        protected override void LogInternal<T>(LogData<T> logData)
        {
            System.Diagnostics.Debug.WriteLine(logData.MachineName);
        }

        protected override async Task LogInternalAsync<T>(LogData<T> logData)
        {
            await Task.Run(() => System.Diagnostics.Debug.WriteLine(logData.User));

        }
    }
}
