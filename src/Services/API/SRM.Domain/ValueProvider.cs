using Fix;
using SRM.Data;
using System;

namespace SRM.Domain.Services
{
    public class ValueProvider : IValueProvider
    {
        private readonly IWorkContext workContext;

        public ValueProvider(IWorkContext workContext)
        {
            this.workContext = workContext ?? throw new ArgumentNullException(nameof(workContext));
        }

        public long CompanyId()
        {
            return workContext.AuthenticationProvider.GetCompanyId();
        }

        public DateTime CreatedOn()
        {
            return DateTime.Now;
        }

        public DateTime CreatedOnUtc()
        {
            return CreatedOn().ToUniversalTime();
        }

        public DateTime ModifiedOn()
        {
            return DateTime.Now;
        }

        public DateTime ModifiedOnUtc()
        {
            return ModifiedOn().ToUniversalTime();
        }

        public long UserId()
        {
            return workContext.AuthenticationProvider.GetUserId();
        }
    }
}
