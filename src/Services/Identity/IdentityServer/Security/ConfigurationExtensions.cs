using Microsoft.Extensions.Configuration;

namespace IdentityServer.Security
{
    public static class ConfigurationExtensions
    {
        private const string KEYS_SECRET_KEY = "Authentication.SecretKey";
        public static string GetSecretKey(this IConfiguration configuration)
        {
            var key = configuration.GetValue<string>(KEYS_SECRET_KEY);
            return key;
        }
    }
}
