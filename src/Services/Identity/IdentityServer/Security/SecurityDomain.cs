using IdentityServer.Security.Services;
using System;

namespace IdentityServer.Security
{
    public class SecurityDomain : ISecurityDomain
    {
        public SecurityDomain(IAccountService accountService)
        {
            Account = accountService ?? throw new ArgumentNullException(nameof(accountService));
        }

        public IAccountService Account { get; }
    }
}
