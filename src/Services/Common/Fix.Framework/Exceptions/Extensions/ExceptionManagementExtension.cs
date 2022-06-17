using Fix.Exceptions.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fix.Exceptions.Extensions
{
    public static class ExceptionManagementExtension
    {


        public static ExceptionConfig GetExceptionConfig(this IConfiguration configuration, string name)
        {
            var section = configuration.GetSection(name);
            var config = new ExceptionConfig();
            section.Bind(config);
            return config;
        }

        public static IServiceCollection UseExceptions(this IServiceCollection services, ExceptionConfig config)
        {
            if (config.IsValid(out string message))
            {
                services.AddSingleton(config);
            }
            return services;
        }
    }
}
