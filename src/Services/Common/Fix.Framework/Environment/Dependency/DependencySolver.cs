using Microsoft.AspNetCore.Http;
using System;

namespace Fix.Environment.Dependency
{
    public class DependencySolver : IDependencySolver
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IHttpContextAccessor contextAccessor;

        public DependencySolver(IServiceProvider serviceProvider, IHttpContextAccessor contextAccessor)
        {
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            this.contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
        }
        public T Get<T>()
        {

            if (contextAccessor.HttpContext != null)
            {
                return (T)contextAccessor.HttpContext.RequestServices.GetService(typeof(T));
            }
            else
            {
                return (T)serviceProvider.GetService(typeof(T)); //contextAccessor.HttpContext.RequestServices.GetService(type);
            }
            //return (T)contextAccessor.HttpContext.RequestServices.GetService(typeof(T));
        }

        public object Get(Type type)
        {

            if (contextAccessor.HttpContext != null)
            {
                return contextAccessor.HttpContext.RequestServices.GetService(type);
            }
            else
            {
                return serviceProvider.GetService(type); //contextAccessor.HttpContext.RequestServices.GetService(type);
            }
        }
    }
}
