using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Fix.Mvc.Middleware
{
    public interface IMiddlewareComponent : IScoped
    {
        int SequenceNo { get; }
        bool CanInvoke { get; }

        Task<bool> OnRequest(HttpContext context);
        Task<bool> OnResponse(HttpContext context);
    }
}
