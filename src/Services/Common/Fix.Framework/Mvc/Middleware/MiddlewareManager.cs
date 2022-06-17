using Fix.Exceptions;
using Fix.Mvc.Middleware.Config;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fix.Mvc.Middleware
{
    public class MiddlewareManager : IMiddlewareManager
    {
        private readonly IExceptionManager exceptionManager;
        private readonly IEnumerable<IMiddlewareComponent> components;
        private readonly MiddlewareConfig config;

        public MiddlewareManager(IExceptionManager exceptionManager, IEnumerable<IMiddlewareComponent> components, MiddlewareConfig config)
        {
            this.exceptionManager = exceptionManager ?? throw new ArgumentNullException(nameof(exceptionManager));
            this.components = components;
            this.config = config;
        }

        public bool UseMiddleware()
        {
            return config.UseMiddleware;
        }

        public async Task<bool> OnRequest(HttpContext context)
        {
            bool shouldNext = true;
            try
            {
                if (components != null)
                {
                    foreach (var component in components.OrderBy(m => m.SequenceNo))
                    {
                        if (!await component.OnRequest(context))
                        {
                            shouldNext = false;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await exceptionManager.HandleAsync(ex);
                shouldNext = true;
            }
            return shouldNext;
        }

        public async Task<bool> OnResponse(HttpContext context)
        {
            bool shouldNext = true;
            try
            {
                if (components != null)
                {
                    foreach (var component in components.OrderBy(m => m.SequenceNo))
                    {
                        if (!await component.OnResponse(context))
                        {
                            shouldNext = false;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await exceptionManager.HandleAsync(ex);
                shouldNext = true;
            }
            return shouldNext;
        }
    }
}

