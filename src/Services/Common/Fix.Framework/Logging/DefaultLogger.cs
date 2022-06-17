using Fix.Logging.Policies;
using System;
using System.Threading.Tasks;

namespace Fix.Logging
{
    public class DefaultLogger : IChainLogger
    {
        private readonly IPolicyProvider policyProvider;
        private readonly IPolicyPerformer policyPerformer;
        private readonly ILogDataFactory logDataFactory;

        public DefaultLogger(
            IPolicyProvider policyProvider,
            IPolicyPerformer policyPerformer,
            ILogDataFactory logDataFactory)
        {
            this.policyProvider = policyProvider ?? throw new ArgumentNullException(nameof(policyProvider));
            this.policyPerformer = policyPerformer ?? throw new ArgumentNullException(nameof(policyPerformer));
            this.logDataFactory = logDataFactory ?? throw new ArgumentNullException(nameof(logDataFactory));
        }

        public T CreatePolicy<T>() where T : BasePolicy
        {
            var policy = policyProvider.Get(typeof(T));
            return (T)policy;
        }

        public void Debug(string debug)
        {
            Debug<object>(null, debug);
        }
        public void Debug<T>(T debug, string message = null) where T : class
        {
            Log(debug, CreatePolicy<DebugPolicy>(), message);
        }

        public async Task DebugAsync(string debug)
        {
            await DebugAsync<string>(debug);
        }

        public async Task DebugAsync<T>(T debug, string message = null) where T : class
        {
            await LogAsync(debug, CreatePolicy<DebugPolicy>(), message);
        }

        public void Error(string message)
        {
            Error<object>(null, message);
        }

        public void Error<T>(T error, string message = null) where T : class
        {
            Log(error, CreatePolicy<ErrorPolicy>(), message);
        }

        public async Task ErrorAsync(string message)
        {
            await ErrorAsync<object>(null, message);
        }
        public async Task ErrorAsync<T>(T error, string message = null) where T : class
        {
            await LogAsync(error, CreatePolicy<InfoPolicy>(), message);
        }

        public void Fatal(string message)
        {
            Fatal<object>(null, message);
        }
        public void Fatal<T>(T fatal, string message = null) where T : class
        {
            Log(fatal, CreatePolicy<FatalPolicy>());
        }

        public async Task FatalAsync(Exception exception)
        {
            await FatalAsync<Exception>(exception);
        }

        public async Task FatalAsync(string fatal)
        {
            await FatalAsync<string>(fatal);
        }

        public async Task FatalAsync<T>(T fatal, string message = null) where T : class
        {
            await LogAsync(fatal, CreatePolicy<FatalPolicy>(), message);
        }

        public async Task FatalAsync(Exception exception, string message = null)
        {
            await FatalAsync<Exception>(exception, message);
        }

        public void Info(string info)
        {
            Info<object>(null, info);
        }

        public void Info<T>(T info, string message = null) where T : class
        {
            Log(info, CreatePolicy<InfoPolicy>(), message);
        }

        public async Task InfoAsync(string info)
        {
            await InfoAsync<object>(null, info);
        }

        public async Task InfoAsync<T>(T info, string message = null) where T : class
        {
            await LogAsync(info, CreatePolicy<InfoPolicy>(), message);
        }

        public void Trace(string trace)
        {
            Trace<string>(trace);
        }

        public void Trace<T>(T trace, string message = null) where T : class
        {
            Log(trace, CreatePolicy<TracePolicy>(), message);
        }

        public async Task TraceAsync(string trace)
        {
            await TraceAsync<string>(trace);
        }

        public async Task TraceAsync<T>(T trace, string message = null) where T : class
        {
            await LogAsync(trace, CreatePolicy<TracePolicy>());
        }

        public void Warning(string warning)
        {
            Warning<string>(warning);
        }

        public void Warning<T>(T warning, string message = null) where T : class
        {
            Log(warning, CreatePolicy<WarnPolicy>());
        }

        public async Task WarningAsync(string warning)
        {
            await WarningAsync<string>(warning);
        }

        public async Task WarningAsync<T>(T warning, string message = null) where T : class
        {
            await LogAsync(warning, CreatePolicy<WarnPolicy>(), message);
        }

        public void Log<T>(T instance, BasePolicy policy, string message = null) where T : class
        {
            var data = logDataFactory.Create(policy.Level, instance, message);
            policyPerformer.Perform(policy, data);
        }

        public async Task LogAsync<T>(T instance, BasePolicy policy, string message = null) where T : class
        {
            var data = logDataFactory.Create(policy.Level, instance, message);
            await policyPerformer.PerformAsync(policy, data);
        }

    }
}
