using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace IdentityServer.Infrastructor.Configuration
{
    public abstract class DesignTimeDbContextFactoryBase<TContext> :
        IDesignTimeDbContextFactory<TContext> where TContext : DbContext
    {
        public TContext CreateDbContext(string[] args)
        {
            return Create(
                Directory.GetCurrentDirectory(),
                System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
        }
        protected abstract TContext CreateNewInstance(
            DbContextOptions<TContext> options);

        public TContext Create()
        {
            var environmentName =
                System.Environment.GetEnvironmentVariable(
                    "ASPNETCORE_ENVIRONMENT");

            var basePath = AppContext.BaseDirectory;

            return Create(basePath, environmentName);
        }

        private TContext Create(string basePath, string environmentName)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json", true)
                .AddEnvironmentVariables();
            var configuration = builder.Build();
            var section = configuration.GetSection("DatabaseConfig");
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

            return Create(config);
        }

        private TContext Create(DatabaseConfig databaseConfig)
        {
            if (string.IsNullOrEmpty(databaseConfig.ConnectionString))
                throw new ArgumentException(
             $"{nameof(databaseConfig.Provider)} is null or empty.",
             nameof(databaseConfig.Provider));
            var optionsBuilder =
                 new DbContextOptionsBuilder<TContext>();
            Console.WriteLine("DesignTimeDbContextFactoryBase.Create(string): Connection string: {0}",
                databaseConfig.ConnectionString);

            //optionsBuilder.UseSqlServer(databaseConfig.ConnectionString);
            if (databaseConfig.Provider == "PostgreSql")
                optionsBuilder.UseNpgsql<TContext>(databaseConfig.ConnectionString);
            DbContextOptions<TContext> options = optionsBuilder.Options;
            return CreateNewInstance(options);
        }
    }
}
