using Fix.Security;
using System.Collections.Generic;

namespace SRM.Services.Api
{
    public class Permissions : IPermissionProvider
    {
        public IEnumerable<string> GetPermissions()
        {
            return null;
        }
    }
}
