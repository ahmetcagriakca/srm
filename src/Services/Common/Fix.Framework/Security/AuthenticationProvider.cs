using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Fix.Security
{
    public class AuthenticationProvider : IAuthenticationProvider
    {
        private readonly IHttpContextAccessor _httpContext;

        public AuthenticationProvider(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public bool IsAuthenticated => _httpContext.HttpContext.User.Identity.IsAuthenticated;

        public IEnumerable<string> GetUserPermissions()
        {
            var claim = _httpContext.HttpContext.User;
            var claimsIdentity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
            var uid = Convert.ToInt32(claimsIdentity.Claims.FirstOrDefault(c => c.Type == "sub")?.Value);
            return null;
        }

        public long GetUserId()
        {
            if (IsAuthenticated)
            {
                var claimsIdentity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
                var claim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == "uid");
                if (claim != null)
                {
                    return claim.Value.ToLong();
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        public string GetUserName()
        {
            if (IsAuthenticated)
            {
                var claimsIdentity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
                var un = claimsIdentity.Claims.FirstOrDefault(c => c.Type == "un")?.Value;
                return un;
            }
            else
            {
                return string.Empty;
            }
        }

        public long GetCompanyId()
        {
            if (IsAuthenticated)
            {
                var claimsIdentity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
                var claim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == "cid");
                if (claim != null)
                {
                    return claim.Value.ToLong();
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
    }
}
