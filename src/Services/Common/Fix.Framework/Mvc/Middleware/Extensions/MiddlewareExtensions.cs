using Fix.Mvc.Middleware.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fix.Mvc.Middleware.Extensions
{

    public static class MiddlewareExtensions
    {

        public static MiddlewareConfig GetMiddlewareConfig(this IConfiguration configuration, string name)
        {
            var section = configuration.GetSection(name);
            var config = new MiddlewareConfig();
            section.Bind(config);

            return config;
        }

        public static IServiceCollection UseFixMiddleware(this IServiceCollection services, MiddlewareConfig config)
        {
            if (config.IsValid(out string message))
            {
                services.AddSingleton(config);
            }
            return services;
        }

        public static IApplicationBuilder UseFixMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MainMiddleware>();
        }
    }
}
