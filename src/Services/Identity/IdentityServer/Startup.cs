using System.Configuration;
using System.IO;
using System.Net.Http;
using Autofac;
using Fix.Environment.Extensions;
using Fix.Environment.FeatureManagement;
using Fix.Environment.FileSystem;
using Fix.Environment.Shell;
using Fix.Exceptions.Extensions;
using Fix.Mvc;
using Fix.Mvc.Filters.Extensions;
using Fix.Validation.Extensions;
using IdentityServer.Infrastructor;
using IdentityServer.UserServices;
using IdentityServer.Utility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using System.Security.Cryptography.X509Certificates;
using IdentityServer;

namespace IdentityServer
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup : BaseStartup
    {

        /// <summary>
        /// 
        /// </summary>
        public IHostEnvironment Environment { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="environment"></param>
        public Startup(IConfiguration configuration, IHostEnvironment environment) : base(configuration)
        {
            Environment = environment;

        }
        public static HttpMessageHandler Handler { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            #region  Identity server configuration
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedHost;
            });
            var identityConfig = Configuration.GetIdentityConfig("IdentityConfig");
            //Identity server certificate signing
            var builderIdentity = services.AddIdentityServer(options =>
            {
                options.PublicOrigin = identityConfig.Authority;
                options.Events.RaiseSuccessEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseErrorEvents = true;

            })
            .AddInMemoryIdentityResources(Config.GetIdentityResources())
            .AddInMemoryApiResources(Config.GetApis())
            .AddInMemoryClients(Config.GetClients())
            .AddCustomUserStore();

            if (Environment.IsDevelopment())
            {
                builderIdentity.AddDeveloperSigningCredential();
            }
            else
            {
                var contentRootPath = Environment.ContentRootPath;
                var certificate = new X509Certificate2(Path.Combine(contentRootPath, identityConfig.CertificatePath), identityConfig.CertificatePassword);
                //var certificate = new X509Certificate2(identityConfig.CertificatePath, identityConfig.CertificatePassword);
                builderIdentity.AddSigningCredential(certificate);
            }

            #endregion
            #region Identity server api 
            services
               .UseSerilog(Configuration)
               .AddCors(o => o.AddPolicy("AllowAll", builder =>
               {
                   builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
               }))
               .UseIdentiyServerAuthentication(Configuration.GetIdentityConfig("IdentityConfig"), Handler)
               .UseEntityFramework<IdentityServerDbContext>(Configuration.GetDatabaseConfig("DatabaseConfig"))
               .UseExceptions(Configuration.GetExceptionConfig("ExceptionConfig"))
               .UseFixConfiguration()
               .AddNewtonsoftJson(options =>
               {
                   options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                   options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
               })
               .AddFilters()
               .UseFluentValidation();
            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public virtual void Configure(IApplicationBuilder app, IHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            Log.ForContext<Startup>()
                .Information("<{EventID:l}> Configure Started on {Env} {Application} with {@configuration}",
                    "Startup", env.EnvironmentName, env.ApplicationName, Configuration);
            loggerFactory.AddSerilog();
            app
                .UseAuthentication()
                .UseCors("AllowAll")
                .UseRouting()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                })
                .UseSwagger()
                .UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "IdenetityServer API V1");
                    options.OAuthClientId("client");
                    options.OAuthClientSecret("secret");
                    options.RoutePrefix = string.Empty;
                });

            app.UseStaticFiles();

            app.UseIdentityServer();

        }
    }
}