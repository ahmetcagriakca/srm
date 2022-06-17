using System;
using System.IO;
using Autofac.Extensions.DependencyInjection;
using Fix.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using EnvironmentName = Microsoft.AspNetCore.Hosting.EnvironmentName;

namespace IdentityServer.IntegrationTest
{
    public class FakeStartup : Startup
    {
        public FakeStartup(IConfiguration configuration, IHostEnvironment environment) : base(configuration, environment)
        {  var identityUrl = "http://localhost/";//"http://35.205.187.151:100";//"http://localhost:65341"//"https://ahmetcagriakca.com"
            
        }

        public override void Configure(IApplicationBuilder app, IHostEnvironment env, ILoggerFactory loggerFactory)
        {
            base.Configure(app, env, loggerFactory);

            var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var serviceScope = serviceScopeFactory.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<IDbContextLocator>();

                if (dbContext.Current.Database.GetDbConnection().Database.Contains("Prod"))
                {
                    throw new Exception("LIVE SETTINGS IN TESTS!");
                }

                // Initialize database
            }
        }
    }
    public class IdentityServerFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
    {
        //public IdentityServerFactory()
        //{
        //    this.CreateHostBuilder().ConfigureHostConfiguration(builder => );
        //    //builderConfigurationRootbuilder.UseSolutionRelativeContentRoot("tests/IntegrationTest/Services/IdentityServer.IntegrationTest")
        //}
        private IConfigurationRoot _configurationRoot;

        public IConfigurationRoot ConfigurationRoot
        {
            get
            {
                return _configurationRoot ??= new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("integrationsettings.json", optional: true)
                    .Build();
            }
        }

        protected override IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder(null)
                .UseEnvironment("Development")
                .UseContentRoot("tests/IntegrationTest/Services/IdentityServer.IntegrationTest")
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseConfiguration(ConfigurationRoot)
                        .UseStartup<TEntryPoint>()
                        .UseUrls("http://localhost");
                });
        }
    }
}
