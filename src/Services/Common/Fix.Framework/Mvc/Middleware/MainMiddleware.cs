using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Fix.Mvc.Middleware
{

    public class MainMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IMiddlewareManager middlewareManager;

        public MainMiddleware(RequestDelegate next, IMiddlewareManager middlewareManager)
        {
            this.next = next;
            this.middlewareManager = middlewareManager ?? throw new ArgumentNullException(nameof(middlewareManager));
        }

        public async Task Invoke(HttpContext context)
        {
            if (middlewareManager.UseMiddleware())
            {
                if (await middlewareManager.OnRequest(context))
                {
                    Stream originalBody = context.Response.Body;
                    try
                    {
                        using (var memStream = new MemoryStream())
                        {
                            context.Response.Body = memStream;
                            await next(context)
                                .ContinueWith(action => middlewareManager.OnResponse(context));
                            memStream.Position = 0;
                            await memStream.CopyToAsync(originalBody);
                        }
                    }
                    finally
                    {
                        context.Response.Body = originalBody;
                    }
                }
            }
            else
            {
                await next(context);
            }
        }
    }
}
