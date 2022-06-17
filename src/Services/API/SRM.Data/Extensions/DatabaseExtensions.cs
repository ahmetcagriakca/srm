using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SRM.Data.Configuration;
using System;

namespace SRM.Data.Extensions
{
    public static class DatabaseExtensions
    {
        public static DatabaseConfig GetDatabaseConfig(this IConfiguration configuration, string name)
        {
            var section = configuration.GetSection(name);
            var config = new DatabaseConfig();
            section.Bind(config);
            if (configuration["DATABASE_PROVIDER"] != null)
            {
                config.Provider = configuration["DATABASE_PROVIDER"];
            }
            Console.WriteLine("Environment value " + configuration["DATABASE_CONNECTION_STRING"]);
            if (configuration["DATABASE_CONNECTION_STRING"] != null)
            {
                config.ConnectionString = configuration["DATABASE_CONNECTION_STRING"];
            }

            return config;
        }

        public static IServiceCollection UseEntityFramework<T>(this IServiceCollection services, DatabaseConfig configuration) where T : DbContext
        {
            if (configuration.Provider == "PostgreSql")
                return services.AddEntityFrameworkNpgsql().AddDbContext<SrmDbContext>(options => options.UseNpgsql(configuration.ConnectionString));

            if (configuration.Provider == "MSSql" || configuration.Provider == "SqlExpress")
                return services.AddDbContext<SrmDbContext>(options => options.UseSqlServer(configuration.ConnectionString));

            throw new Exception("InvalidCastException provider name");
        }
    }

}
