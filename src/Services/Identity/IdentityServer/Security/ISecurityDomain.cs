using Fix;
using IdentityServer.Security.Services;

namespace IdentityServer.Security
{
    public interface ISecurityDomain : IDependency
    {
        IAccountService Account { get; }
    }
}
