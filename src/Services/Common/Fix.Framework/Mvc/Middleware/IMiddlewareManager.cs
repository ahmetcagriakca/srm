using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Fix.Mvc.Middleware
{
    public interface IMiddlewareManager : IDependency
    {
        bool UseMiddleware();
        Task<bool> OnRequest(HttpContext context);
        Task<bool> OnResponse(HttpContext context);
    }
}
