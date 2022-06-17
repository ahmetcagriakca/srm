using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace Fix.Mvc.Filters
{
    public class FixAuthorization : Attribute, IAuthorizationFilter
    {
        public string Permission { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.Filters.Any(item => item is IAllowAnonymousFilter))
            {
                //TODO:var workContext = context.HttpContext.RequestServices.GetService<IWorkContext>();
                if (context.HttpContext.User.Identity.IsAuthenticated)
                {
                    //TODO:
                    //if (!Permission.IsNullOrEmpty() && !workContext.Authorizer.Authorize(Permission))
                    //{
                    //    var builder = workContext.DependencySolver.Get<IActionResultBuilder>();
                    //    context.Result = builder.Build($"Forbidden access", "{Permission}", StatusCodes.Status403Forbidden);
                    //    return;
                    //}
                    //else 
                    return;
                }
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
