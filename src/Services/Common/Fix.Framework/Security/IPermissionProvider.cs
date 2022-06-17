using System.Collections.Generic;

namespace Fix.Security
{
    public interface IPermissionProvider : IScoped
    {
        IEnumerable<string> GetPermissions();
    }
}
