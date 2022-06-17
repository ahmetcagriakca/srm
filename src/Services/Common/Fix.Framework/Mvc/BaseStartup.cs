using Microsoft.Extensions.Configuration;

namespace Fix.Mvc
{
    public abstract class BaseStartup
    {
        protected IConfiguration Configuration { get; }
        public BaseStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfigurationSection GetSection(string sectionName)
        {
            return Configuration.GetSection(sectionName);
        }
    }
}
