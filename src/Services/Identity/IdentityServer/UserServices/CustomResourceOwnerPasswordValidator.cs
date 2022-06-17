using IdentityModel;
using IdentityServer.Models;
using IdentityServer.Security.Models;
using IdentityServer.Security.Services;
using IdentityServer4.Validation;
using System.Threading.Tasks;

namespace IdentityServer.UserServices
{
    public class CustomResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IAccountService accountService;

        public CustomResourceOwnerPasswordValidator(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            if (accountService.TryGetUserContext(context.UserName, context.Password, out ClientContext clientContext, out User user))
            {
                //var user = accountService.GetByUserName(context.UserName);
                context.Result = new GrantValidationResult(user.Id.ToString(), OidcConstants.AuthenticationMethods.Password);
            }
            return Task.FromResult(0);
        }
    }
}
