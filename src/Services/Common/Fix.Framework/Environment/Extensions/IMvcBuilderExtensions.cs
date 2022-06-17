using Microsoft.Extensions.DependencyInjection;

namespace Fix.Environment.Extensions
{
    public static class IMvcBuilderExtensions
    {
        public static IMvcBuilder UseFixConfiguration(this IServiceCollection services)
        {
            return services
                .AddMvc();
        }
    }
}
