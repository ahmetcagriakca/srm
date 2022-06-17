using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;
using Serilog.Sinks.SystemConsole.Themes;
using System;

namespace SRM.Services.Api.Utility
{
    public static class LoggingExtensions
    {
        public static IServiceCollection UseSerilog(this IServiceCollection services, IConfiguration configuration)
        {

            #region ElasticSearchLoggerConfigurations
            var elasticUri = configuration["ElasticConfiguration:Uri"];
            var outputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u4}] | {Message:l}{NewLine}{Exception}";

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Application", "identityserver.srm.com")
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .WriteTo.File($"logs/srm-log-{DateTime.Now:yyyy-MM-dd_HH:mm:ss}.log",
                    outputTemplate: outputTemplate,
                    fileSizeLimitBytes: 100_000_000,
                    rollOnFileSizeLimit: true)
                .WriteTo.Elasticsearch(
                    new ElasticsearchSinkOptions(
                        new Uri(elasticUri))
                    {
                        CustomFormatter = new ExceptionAsObjectJsonFormatter(renderMessage: true),
                        AutoRegisterTemplate = true,
                        TemplateName = "serilog-events-template",
                        IndexFormat = "srm-log-{0:yyyy.MM.dd}"
                    })
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Literate)
                .MinimumLevel.Verbose()
                .CreateLogger();
            Log.Information("Api started ...");
            #endregion

            return services;
        }
    }
}
