using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using IdentityServer.Infrastructor.Configuration;

namespace IdentityServer.Utility
{
    public static class LoggingExtension
    {


        public static IServiceCollection UseSerilog(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("DatabaseConfig");
            var config = new DatabaseConfig();
            section.Bind(config);
            #region ElasticSearchLoggerConfigurations
            var elasticUri = configuration["ElasticConfiguration:Uri"];
            var outputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u4}] | {Message:l}{NewLine}{Exception}";

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Application", "identityserver.srm.com")
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Literate)
                .MinimumLevel.Verbose()
                .CreateLogger();
            Log.Information("Api started ...");
            #endregion

            return services;
        }
    }
}
