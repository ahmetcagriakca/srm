using IdentityServer.Security.Services;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer.UserServices
{
    public class CustomProfileService : IProfileService
    {
        protected readonly IAccountService _accountService;

        public CustomProfileService(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = _accountService.GetUser(long.Parse(sub));
            var claims = new List<Claim>
                {
                    new Claim("uid", user.Id.ToString()),
                    new Claim("un", user.UserName),
                    new Claim("cid", user.Company.Id.ToString()),
                    new Claim("name", user.Name),
                    new Claim("surname", user.Surname),
                    new Claim("apiurl", user.Company.ApiUrl),
                };
            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = _accountService.GetUser(long.Parse(sub));
            context.IsActive = (user != null && user.IsActive);
        }
    }
}
