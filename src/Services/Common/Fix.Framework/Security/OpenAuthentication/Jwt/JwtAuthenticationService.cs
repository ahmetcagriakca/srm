using Fix.Caching;
using Fix.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace Fix.Security.OpenAuthentication.Jwt
{
    public class JwtAuthenticationService //: IAuthenticationService
    {
        private const string TOKEN_KEY = "Authorization";

        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IJwtTokenBuilder jwtTokenBuilder;
        private readonly IJwtTokenValidator jwtTokenValidator;
        private readonly IHeaderValueProvider headerValueProvider;
        private readonly ICacheManager cacheManager;

        protected IClientContext Context { get; set; }


        public JwtAuthenticationService(
            IHeaderValueProvider headerValueProvider,
            ICacheManager cacheManager,
            IHttpContextAccessor httpContextAccessor,
            IJwtTokenBuilder jwtTokenBuilder,
            IJwtTokenValidator jwtTokenValidator
            )
        {
            this.headerValueProvider = headerValueProvider ?? throw new ArgumentNullException(nameof(headerValueProvider));
            this.cacheManager = cacheManager ?? throw new ArgumentNullException(nameof(cacheManager));
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            this.jwtTokenBuilder = jwtTokenBuilder ?? throw new ArgumentNullException(nameof(jwtTokenBuilder));
            this.jwtTokenValidator = jwtTokenValidator ?? throw new ArgumentNullException(nameof(jwtTokenValidator));
        }

        public bool IsAuthenticated
        {
            get
            {
                return !(GetContext<IClientContext>() == default);
            }
        }

        public void InvalidateContext()
        {
            if (Context != null)
            {
                var key = GetCacheKey(Context.Key);
                cacheManager.Remove(key);
                Context = null;
            }
        }

        public TContext GetContext<TContext>() where TContext : IClientContext
        {
            if (Context == null)
            {
                if (headerValueProvider.TryGet(TOKEN_KEY, out string token))
                {
                    if (TryValidate(token, out ClaimsPrincipal claimsPrincipal))
                    {
                        httpContextAccessor.HttpContext.User = claimsPrincipal;
                        var key = GetCacheKey(httpContextAccessor.HttpContext.User.Identity.Name);
                        Context = cacheManager.Get<TContext>(key);
                    }
                }
            }
            return (TContext)Context;
        }


        public T CreateIdentity<T>(IClientContext clientContext) where T : IIdentityContext
        {
            var context = jwtTokenBuilder.Create<T>(clientContext.Key, out string jwtToken);
            SetToCache(clientContext);
            if (TryValidate(jwtToken, out ClaimsPrincipal claimsPrincipal))
            {
                httpContextAccessor.HttpContext.User = claimsPrincipal;
                var key = GetCacheKey(httpContextAccessor.HttpContext.User.Identity.Name);
                Context = clientContext;
            }
            return context;
        }

        private void SetToCache(IClientContext payload)
        {
            var key = GetCacheKey(payload.Key);
            cacheManager.Set(key, payload, 1000 * 60 * 10000);
        }

        private string GetCacheKey(string uniqueName)
        {
            return $"User_({uniqueName})";
        }
        private bool TryValidate(string token, out ClaimsPrincipal claimsPrincipal)
        {
            bool isValid = false;
            claimsPrincipal = null;
            try
            {
                claimsPrincipal = jwtTokenValidator.Validate(token);
                isValid = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex);
            }
            return isValid;
        }


    }
}
