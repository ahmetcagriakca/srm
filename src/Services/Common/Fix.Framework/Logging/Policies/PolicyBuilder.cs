using Fix.Logging.Config;
using Fix.Logging.Iteration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Fix.Logging.Policies
{
    public class PolicyBuilder
    {
        private readonly LoggingConfig loggingConfig;

        public PolicyBuilder(LoggingConfig loggingConfig)
        {
            this.loggingConfig = loggingConfig ?? throw new ArgumentNullException(nameof(loggingConfig));
        }

        public object Build(LogLevel logLevel, IServiceProvider serviceProvider)
        {

            var instance = Activator.CreateInstance(logLevel.PolicyType()) as BasePolicy;
            var policy = loggingConfig.Policies.Where(x => x.LogLevel == instance.Level.ToString()).FirstOrDefault();
            if (policy != null && policy.IsEnabled == "1")
            {
                foreach (var logger in serviceProvider.GetServices<ILogger>())
                {
                    if (policy.LoggerAlias.Contains(logger.AliasName))
                    {
                        instance.AddLoggerType(logger.GetType());
                    }
                }

                instance.Iterator = CreateIterator(policy.IterationType);
            }
            return instance;
        }



        private ILogIterator CreateIterator(string value)
        {

            switch (value)
            {
                case "LogOne": return new LogOneIterator();
                case "Parallel": return new ParallelIterator();
                default: return new SequentialIterator();
            }
        }




    }
}