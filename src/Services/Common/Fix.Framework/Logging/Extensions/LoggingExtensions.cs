using Fix.Logging.Config;
using Fix.Logging.Policies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Fix.Logging.Extensions
{
    public static class LoggingExtensions
    {
        public static LoggingConfig GetLoggingConfig(this IConfiguration configuration, string name)
        {
            var section = configuration.GetSection(name);
            var config = new LoggingConfig();
            section.Bind(config);

            return config;
        }

        public static IServiceCollection UseLogging(this IServiceCollection services, LoggingConfig config)
        {
            if (config.IsValid(out string message))
            {
                //services.AddScoped<LoggingIndex>((x) => new LoggingIndex(config.ElasticsearchConfig));
                services.AddSingleton(config);
                var builder = new PolicyBuilder(config);

                foreach (LogLevel level in (LogLevel[])Enum.GetValues(typeof(LogLevel)))
                {
                    services.AddSingleton(level.PolicyType(), provider => builder.Build(level, provider));
                }
            }
            return services;
        }
    }
}
