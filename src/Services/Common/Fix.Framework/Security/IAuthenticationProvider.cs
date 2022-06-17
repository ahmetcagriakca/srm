using System.Collections.Generic;

namespace Fix.Security
{
    public interface IAuthenticationProvider : IDependency
    {
        bool IsAuthenticated { get; }

        IEnumerable<string> GetUserPermissions();

        long GetUserId();

        string GetUserName();

        long GetCompanyId();
    }
}
