using IdentityServer4.Validation;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer.UserServices
{
    public class CustomTokenValidator : ICustomTokenRequestValidator

    {
        public Task ValidateAsync(CustomTokenRequestValidationContext context)
        {
            var x = context.Result;
            context.Result.ValidatedRequest.Client.AlwaysSendClientClaims = true;
            context.Result.ValidatedRequest.ClientClaims.Add(new Claim("testtoken", "testbody"));
            return Task.FromResult(0);
        }
    }
    public class CustomAuthorizeRequestValidator : ICustomAuthorizeRequestValidator
    {
        public Task ValidateAsync(CustomAuthorizeRequestValidationContext context)
        {
            var x = context.Result;
            return Task.FromResult(0);
        }
    }
}
