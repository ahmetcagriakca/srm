using Fix.Configuration.Exceptions;
using Fix.Security.OpenAuthentication.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fix.Security.Extensions
{
    public static class AuthenticationExtensions
    {
        //public static IServiceCollection AddWindowsAuth(this IServiceCollection services, IConfiguration configuration, string sectionName)
        //{
        //    var section = configuration.GetSection(sectionName);
        //    return services.AddWindowsAuth(section);
        //}
        //public static IServiceCollection AddWindowsAuth(this IServiceCollection services, IConfigurationSection configurationSection)
        //{
        //    var config = new WindowsConfig();
        //    configurationSection.Bind(config);
        //    return services.AddWindowsAuth(config);
        //}

        //public static IServiceCollection AddWindowsAuth(this IServiceCollection services, WindowsConfig config)
        //{
        //    if (config == null || !config.IsValid())
        //    {
        //        throw new ConfigurationNotValidException(nameof(config));
        //    }

        //    return services
        //        .AddSingleton(typeof(WindowsConfig), config)
        //        .AddScoped(typeof(IAuthenticationService), typeof(WindowsAuthenticationService));

        //}

        public static JwtConfig GetJwtConfig(this IConfiguration configuration, string name)
        {
            var section = configuration.GetSection(name);
            var config = new JwtConfig();
            section.Bind(config);
            return config;
        }

        public static IServiceCollection UseJwtAuthentication(this IServiceCollection services, IConfiguration configuration, string sectionName)
        {
            var section = configuration.GetSection(sectionName);
            return services.UseJwtAuthentication(section);
        }

        public static IServiceCollection UseJwtAuthentication(this IServiceCollection services, IConfigurationSection configurationSection)
        {
            var config = new JwtConfig();
            configurationSection.Bind(config);
            return services.UseJwtAuthentication(config);
        }

        public static IServiceCollection UseJwtAuthentication(this IServiceCollection services, JwtConfig config)
        {
            if (config == null || !config.IsValid())
            {
                throw new ConfigurationNotValidException(nameof(config));
            }

            return null;
            //return services
            //.AddSingleton(typeof(JwtConfig), config)
            //.AddScoped(typeof(IAuthenticationService), typeof(JwtAuthenticationService));

        }
    }
}
