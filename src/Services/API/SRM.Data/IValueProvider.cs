using Fix;
using System;

namespace SRM.Data
{

    public interface IValueProvider : IDependency
    {
        DateTime CreatedOn();
        DateTime CreatedOnUtc();
        DateTime ModifiedOnUtc();
        DateTime ModifiedOn();
        long UserId();
        long CompanyId();
    }
}
