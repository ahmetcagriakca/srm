using System;
using System.Threading.Tasks;

namespace Fix.Logging
{
    public abstract class BaseLogger : ILogger
    {
        protected BaseLogger(ILogDataFactory logDataFactory)
        {
            LogDataFactory = logDataFactory ?? throw new ArgumentNullException(nameof(logDataFactory));
        }

        public ILogDataFactory LogDataFactory { get; }

        public abstract string AliasName { get; }

        protected abstract void LogInternal<T>(LogData<T> logData) where T : class;

        protected abstract Task LogInternalAsync<T>(LogData<T> logData) where T : class;

        public void Debug(string debug)
        {
            Debug<string>(debug);
        }

        public void Debug<T>(T debug) where T : class
        {
            Log(debug, LogLevel.Debug);
        }

        public async Task DebugAsync(string debug)
        {
            await DebugAsync<string>(debug);
        }

        public async Task DebugAsync<T>(T debug) where T : class
        {
            await LogAsync(debug, LogLevel.Debug);
        }

        public void Error(string error)
        {
            Error<string>(error);
        }

        public void Error<T>(T error) where T : class
        {
            Log(error, LogLevel.Error);
        }

        public void Error(Exception exception)
        {
            Error<Exception>(exception);
        }

        public async Task ErrorAsync(string error)
        {
            await ErrorAsync<string>(error);
        }

        public async Task ErrorAsync<T>(T error) where T : class
        {
            await LogAsync(error, LogLevel.Error);
        }

        public async Task ErrorAsync(Exception exception)
        {
            await ErrorAsync<Exception>(exception);
        }

        public void Fatal(string fatal)
        {
            Fatal<string>(fatal);
        }

        public void Fatal<T>(T fatal) where T : class
        {
            Log<T>(fatal, LogLevel.Fatal);
        }
        public void Fatal(Exception exception)
        {
            Fatal<Exception>(exception);
        }
        public async Task FatalAsync(string fatal)
        {
            await FatalAsync<string>(fatal);
        }

        public async Task FatalAsync(Exception exception)
        {
            await FatalAsync<Exception>(exception);
        }

        public async Task FatalAsync<T>(T fatal) where T : class
        {
            await LogAsync<T>(fatal, LogLevel.Fatal);
        }

        public void Info(string info)
        {
            Info<string>(info);
        }

        public void Info<T>(T info) where T : class
        {
            Log(info, LogLevel.Info);
        }

        public async Task InfoAsync(string info)
        {
            await InfoAsync<string>(info);
        }

        public async Task InfoAsync<T>(T info) where T : class
        {
            await LogAsync<T>(info, LogLevel.Info);
        }

        public void Trace(string trace)
        {
            Trace<string>(trace);
        }

        public void Trace<T>(T trace) where T : class
        {
            Log(trace, LogLevel.Trace);
        }

        public async Task TraceAsync(string trace)
        {
            await TraceAsync<string>(trace);
        }

        public async Task TraceAsync<T>(T trace) where T : class
        {
            await LogAsync(trace, LogLevel.Trace);
        }

        public void Warning(string warning)
        {
            Warning<string>(warning);
        }

        public void Warning<T>(T warning) where T : class
        {
            Log(warning, LogLevel.Warn);
        }

        public async Task WarningAsync(string warning)
        {
            await WarningAsync<string>(warning);
        }
        public async Task WarningAsync<T>(T warning) where T : class
        {
            await LogAsync(warning, LogLevel.Warn);
        }

        public void Log<T>(LogData<T> logData) where T : class
        {
            LogInternal(logData);
        }

        public Task LogAsync<T>(LogData<T> logData) where T : class
        {
            return LogInternalAsync(logData);
        }

        public void Log<T>(T instance, LogLevel logLevels) where T : class
        {
            var logData = LogDataFactory.Create(logLevels, instance, "");
            LogInternal(logData);
        }

        public async Task LogAsync<T>(T instance, LogLevel logLevels) where T : class
        {
            var logData = LogDataFactory.Create(logLevels, instance, "");
            await LogInternalAsync(logData);
        }
    }
}

