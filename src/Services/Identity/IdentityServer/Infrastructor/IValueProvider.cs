using Fix;
using System;

namespace IdentityServer.Infrastructor
{
    public interface IValueProvider : IDependency
    {
        DateTime CreatedOn();
        DateTime CreatedOnUtc();
        DateTime ModifiedOnUtc();
        DateTime ModifiedOn();
        long UserId();
    }
}
