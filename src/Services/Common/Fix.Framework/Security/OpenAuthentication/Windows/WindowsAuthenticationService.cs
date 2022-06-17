using Fix.Caching;
using Fix.Security.OpenAuthentication.Exceptions;
using Microsoft.AspNetCore.Http;
using System;

namespace Fix.Security.OpenAuthentication.Windows
{
    public class WindowsAuthenticationService //: IAuthenticationService
    {
        private const string TOKEN_KEY = "Authorization";

        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IWindowsContextBuilder windowsContextBuilder;
        private readonly ICacheManager cacheManager;
        private IClientContext _clientContext;

        public bool IsAuthenticated
        {
            get
            {
                return !(GetContext<IClientContext>() == default);
            }
        }


        public WindowsAuthenticationService(
            ICacheManager cacheManager,
            IHttpContextAccessor httpContextAccessor,
            IWindowsContextBuilder windowsContextBuilder
            )
        {
            this.cacheManager = cacheManager ?? throw new ArgumentNullException(nameof(cacheManager));
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            this.windowsContextBuilder = windowsContextBuilder;
        }


        public void InvalidateContext()
        {
            if (_clientContext != null)
            {
                var key = GetCacheKey(_clientContext.Key);
                cacheManager.Remove(key);
                _clientContext = null;
            }
        }

        private bool TryGetUserName(out string userName)
        {
            bool result = false;
            userName = httpContextAccessor.HttpContext.User.Identity.Name;
            if (!string.IsNullOrEmpty(userName))
            {
                userName = userName.Substring(userName.IndexOf("\\") + 1);
                result = true;
            }
            return result;
        }

        public TContext GetContext<TContext>() where TContext : IClientContext
        {
            if (_clientContext == null || _clientContext.Key.IsNullOrEmpty())
            {
                lock (this)
                {
                    if (_clientContext == null || _clientContext.Key.IsNullOrEmpty())
                    {
                        if (TryGetUserName(out string userName))
                        {
                            _clientContext = GetContext<TContext>(userName);
                            if (_clientContext.Key.IsNullOrEmpty())
                            {
                                throw new AuthenticationCacheNotFoundException("Authentication failed. You must be login.");
                            }
                        }
                    }
                }
            }
            return (TContext)_clientContext;
        }

        public TContext GetContext<TContext>(string userName) where TContext : IClientContext
        {
            var key = GetCacheKey(userName);
            return cacheManager.Get<TContext>(key);
        }

        public T CreateIdentity<T>(IClientContext payload) where T : IIdentityContext
        {
            var context = windowsContextBuilder.Create<T>(payload.Key);
            SetToCache(payload);
            return context;
        }

        public bool HasContext()
        {
            if (_clientContext != null)
            {
                if (TryGetUserName(out string userName))
                {
                    return cacheManager.IsSet(GetCacheKey(userName));
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void SetToCache(IClientContext payload)
        {
            _clientContext = payload;
            var key = GetCacheKey(payload.Key);
            cacheManager.Set(key, payload, 1000 * 60 * 24);
        }

        private string GetCacheKey(string uniqueName)
        {
            return $"User_({uniqueName})";
        }
    }
}
