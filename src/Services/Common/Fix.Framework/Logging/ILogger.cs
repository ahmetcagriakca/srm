using System;
using System.Threading.Tasks;

namespace Fix.Logging
{
    public interface ILogger : IScoped
    {
        string AliasName { get; }
        void Debug(string debug);
        void Debug<T>(T debug) where T : class;
        Task DebugAsync(string debug);
        Task DebugAsync<T>(T debug) where T : class;
        void Error(Exception exception);
        void Error(string error);
        void Error<T>(T error) where T : class;
        Task ErrorAsync(Exception exception);
        Task ErrorAsync(string error);
        Task ErrorAsync<T>(T error) where T : class;
        void Fatal(Exception exception);
        void Fatal(string fatal);
        void Fatal<T>(T fatal) where T : class;
        Task FatalAsync(Exception exception);
        Task FatalAsync(string fatal);
        Task FatalAsync<T>(T fatal) where T : class;
        void Info(string info);
        void Info<T>(T info) where T : class;
        Task InfoAsync(string info);
        Task InfoAsync<T>(T info) where T : class;
        void Log<T>(T instance, LogLevel logLevels) where T : class;
        Task LogAsync<T>(T instance, LogLevel logLevels) where T : class;
        void Trace(string trace);
        void Trace<T>(T trace) where T : class;
        Task TraceAsync(string trace);
        Task TraceAsync<T>(T trace) where T : class;
        void Warning(string warning);
        void Warning<T>(T warning) where T : class;
        Task WarningAsync(string warning);
        Task WarningAsync<T>(T warning) where T : class;

        void Log<T>(LogData<T> wrapper) where T : class;
        Task LogAsync<T>(LogData<T> wrapper) where T : class;
    }
}