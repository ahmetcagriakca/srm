using Fix.Logging.Policies;
using System.Threading.Tasks;

namespace Fix.Logging
{
    public interface IChainLogger : IScoped
    {
        void Debug(string debug);
        void Debug<T>(T debug, string message = null) where T : class;

        Task DebugAsync(string debug);
        Task DebugAsync<T>(T debug, string message = null) where T : class;


        void Error(string message);
        void Error<T>(T error, string message = null) where T : class;


        Task ErrorAsync(string message);
        Task ErrorAsync<T>(T error, string message = null) where T : class;

        void Fatal(string message);
        void Fatal<T>(T fatal, string message = null) where T : class;

        Task FatalAsync(string message);
        Task FatalAsync<T>(T fatal, string message = null) where T : class;




        void Info(string info);
        void Info<T>(T info, string message = null) where T : class;

        Task InfoAsync(string info);
        Task InfoAsync<T>(T info, string message = null) where T : class;

        void Trace(string trace);
        void Trace<T>(T trace, string message = null) where T : class;

        Task TraceAsync(string trace);
        Task TraceAsync<T>(T trace, string message = null) where T : class;

        void Warning(string warning);
        void Warning<T>(T warning, string message = null) where T : class;

        Task WarningAsync(string warning);
        Task WarningAsync<T>(T warning, string message = null) where T : class;

        void Log<T>(T instance, BasePolicy policy, string message = null) where T : class;
        Task LogAsync<T>(T instance, BasePolicy policy, string message = null) where T : class;
    }
}
