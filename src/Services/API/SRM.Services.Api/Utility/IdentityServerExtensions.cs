using Fix.Configuration.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
//using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;

namespace SRM.Services.Api.Utility
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

        public static IServiceCollection UseIdentiyServerAuthentication(this IServiceCollection services, IdentityConfig config)
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
                })
                ;
            return services
                .AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1.0",
                        Title = "SRM API",
                        Description = "SRM API",
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
                });

        }
    }
}
