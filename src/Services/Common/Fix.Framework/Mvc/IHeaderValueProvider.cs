using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;

namespace Fix.Mvc
{
    public interface IHeaderValueProvider : IScoped
    {
        bool TryGet(string key, out string value);
        bool TryGet(string key, out StringValues value);
    }
    public class HeaderValueProvider : IHeaderValueProvider
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public HeaderValueProvider(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }
        public bool TryGet(string key, out string value)
        {
            value = null;
            bool success = false;
            if (TryGet(key, out StringValues a))
            {
                value = a.First();
                success = true;
            }
            return success;
        }

        public bool TryGet(string key, out StringValues value)
        {
            if (httpContextAccessor.HttpContext != null)
                return httpContextAccessor.HttpContext.Request.Headers.TryGetValue(key, out value);
            return false;
        }
    }
}
