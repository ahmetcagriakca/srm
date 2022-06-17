using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.UserServices
{
    public static class CustomIdentityServerBuilderExtensions
    {
        public static IIdentityServerBuilder AddCustomUserStore(this IIdentityServerBuilder builder)
        {
            builder.AddProfileService<CustomProfileService>();
            builder.AddResourceOwnerValidator<CustomResourceOwnerPasswordValidator>();
            builder.AddCustomTokenRequestValidator<CustomTokenValidator>();
            builder.AddCustomAuthorizeRequestValidator<CustomAuthorizeRequestValidator>();
            return builder;
        }
    }
}
