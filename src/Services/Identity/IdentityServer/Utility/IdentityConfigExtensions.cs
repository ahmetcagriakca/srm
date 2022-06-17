using Fix.Configuration.Exceptions;
using IdentityServer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

namespace IdentityServer.Utility
{

    public static class IdentityServerExtensions
    {

        public static IdentityConfig GetIdentityConfig(this IConfiguration configuration, string name)
        {
            var section = configuration.GetSection(name);
            var config = new IdentityConfig();
            section.Bind(config);
            return config;
        }

        public static IServiceCollection UseIdentiyServerAuthentication(this IServiceCollection services, IdentityConfig config,HttpMessageHandler Handler)
        {
            if (config == null)
            {
                throw new ConfigurationNotValidException(nameof(config));
            }

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = config.Authority;
                    options.RequireHttpsMetadata = false;
                    options.Audience = config.Audience;  
                    
                    if(Handler != null)
                    {
                        options.BackchannelHttpHandler = Handler;
                        options.Authority = "http://localhost";
                    }
                })
                ;
            return services
                .AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1.0",
                        Title = "IdenetityServer Api",
                        Description = "IdenetityServer api",
                    });
                    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.OAuth2,
                        Flows = new OpenApiOAuthFlows
                        {
                            Password = new OpenApiOAuthFlow
                            {

                                AuthorizationUrl = new Uri($"{config.Authority}/connect/authorize"),
                                Scopes = new Dictionary<string, string> {
                                        { $"{config.Audience}", $"{config.Audience}"}
                                    },
                                TokenUrl = new Uri($"{config.Authority}/connect/token")
                            }
                        }
                    });
                    var filePath = Path.Combine(System.AppContext.BaseDirectory, "IdentityServer.xml");
                    //added xml comments
                    options.IncludeXmlComments(filePath);
                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                            },
                            new[] { "readAccess", "writeAccess" }
                        }
                    });
                });
        }
    }
}
