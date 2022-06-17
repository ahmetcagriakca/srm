using Microsoft.Extensions.Configuration;
using System;

namespace Fix.Configuration
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IConfiguration configuration;


        private const string CACHE_KEY = "Configuration.Key.{key}";


        public ConfigurationService(IConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public T Get<T>(string key)
        {
            return configuration.GetValue<T>(key);
        }

        public T GetSection<T>(string key)
        {
            return configuration.GetSection("AppSettings").GetValue<T>(key);
        }
    }
}
