using Fix;
using IdentityServer.Infrastructor;
using System;

namespace IdentityServer.Security.Services
{
    public class ValueProvider : IValueProvider
    {
        private readonly IWorkContext workContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workContext"></param>
        public ValueProvider(IWorkContext workContext)
        {
            this.workContext = workContext ?? throw new ArgumentNullException(nameof(workContext));
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
