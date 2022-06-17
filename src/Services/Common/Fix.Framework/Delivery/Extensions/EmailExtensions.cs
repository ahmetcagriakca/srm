using Fix.Delivery.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fix.Delivery.Extensions
{
    public static class EmailExtensions
    {
        public static IServiceCollection AddEmailService(this IServiceCollection services, IConfiguration configuration, string sectionName)
        {
            var section = configuration.GetSection(sectionName);
            return services.AddEmailService(section);
        }
        public static IServiceCollection AddEmailService(this IServiceCollection services, IConfigurationSection configurationSection)
        {
            var config = new EmailConfig();
            configurationSection.Bind(config);
            return services.AddEmailService(config);
        }

        public static IServiceCollection AddEmailService(this IServiceCollection services, EmailConfig config)
        {

            return services
                .AddSingleton(typeof(EmailConfig), config)
                .AddTransient<IEmailService, EmailService>();
        }
    }
}
