using Autofac;
using Fix.Environment.Extensions;
using Fix.Environment.FeatureManagement;
using Fix.Environment.FileSystem;
using Fix.Environment.Shell;
using Fix.Exceptions.Extensions;
using Fix.Jobs;
using Fix.Jobs.Extensions;
using Fix.Logging.Extensions;
using Fix.Mvc;
using Fix.Mvc.Filters.Extensions;
using Fix.Validation.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SRM.Data;
using SRM.Data.Extensions;
using SRM.Services.Api.Utility;

namespace SRM.Services.Api
{
    public class Startup : BaseStartup
    {
        public Startup(IConfiguration configuration) : base(configuration)
        {

        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
               .UseSerilog(Configuration)
               .AddCors(o => o.AddPolicy("AllowAll", builder =>
               {
                   builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
               }))
               .UseLogging(Configuration.GetLoggingConfig("LoggingConfig"))
               .UseIdentiyServerAuthentication(Configuration.GetIdentityConfig("IdentityConfig"))
               .UseEntityFramework<SrmDbContext>(Configuration.GetDatabaseConfig("DatabaseConfig"))
               .UseExceptions(Configuration.GetExceptionConfig("ExceptionConfig"))
               .UseQuartz(Configuration.GetQuartzConfig("QuartzConfig"))
               .UseFixConfiguration()
               .AddNewtonsoftJson(options =>
               {
                   options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                   options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
               })
               .AddFilters()
               .UseFluentValidation();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            ITypeFinder typeFinder = new TypeFinder();
            IDepedencyContextBuilder depedencyContextBuilder = new ShellContextBuilder(typeFinder);
            IWorkScopeBuilder workScopeBuilder = new WorkScopeBuilder(depedencyContextBuilder);
            IAssemblyLoader assemblyLoader = new DefaultAssemblyLoader();
            IFeaturePathProvider featurePathProvider = new FeaturePathProvider();
            IFeatureDirectoryService featureDirectoryService = new FeatureDirectoryService(featurePathProvider);
            IFeatureProvider featureProvider = new FeatureProvider(featureDirectoryService);
            IFeatureContextBuilder featureContextBuilder = new FeatureContextBuilder(assemblyLoader, depedencyContextBuilder, featureProvider);
            var featureContext = featureContextBuilder.Build();
            workScopeBuilder.Build(builder, featureContext);
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowAll")
                .UseStaticFiles()
                .UseRouting()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                })
                .UseAuthentication()
                .UseSwagger()
                .UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "SRM API V1");
                    options.OAuthClientId("client");
                    options.OAuthClientSecret("secret");
                    options.RoutePrefix = string.Empty;
                });
            new QuartzStartup().StartJobs(app, Configuration.GetQuartzConfig("QuartzConfig"));
        }
    }
}
