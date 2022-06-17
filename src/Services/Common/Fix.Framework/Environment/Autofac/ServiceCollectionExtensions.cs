using Microsoft.Extensions.DependencyInjection;
using System;

namespace Fix.Environment.Autofac
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceProvider BuildProvider(this IMvcBuilder mvcBuilder, Action<IServiceCollection> services = null)
        {
            return  ServiceProviderFactory.Create(mvcBuilder, services);
        }
    }
}
