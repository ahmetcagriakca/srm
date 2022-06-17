using Autofac;
using Autofac.Extensions.DependencyInjection;
using Fix.Environment.FeatureManagement;
using Fix.Environment.Shell;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Fix.Environment.Autofac
{
    public static class ServiceProviderFactory
    {
        public static AutofacServiceProvider Create(IMvcBuilder mvcBuilder, Action<IServiceCollection> action)
        {
            if (action != null)
                action.Invoke(mvcBuilder.Services);
            ContainerBuilder builder = new ContainerBuilder();
            return new AutofacServiceProvider(CreateComponentContext(mvcBuilder, builder));
        }


        private static ILifetimeScope CreateComponentContext(IMvcBuilder mvcBuilder, ContainerBuilder builder)
        {
            var shellContainer = ShellScopeBuilder.Instance.Build(builder);
            var featureContext = CreateFeatureContext(shellContainer);
            MergeAppParts(mvcBuilder, featureContext);
            var workScope = CreateWorkScope(mvcBuilder, shellContainer, featureContext);
            return workScope;
        }

        private static ILifetimeScope CreateWorkScope(IMvcBuilder mvcBuilder, IContainer shellContainer, FeatureContext featureContext)
        {
            var workScopeBuilder = shellContainer.Resolve<IWorkScopeBuilder>();
            return shellContainer.BeginLifetimeScope(builder =>
            {
                workScopeBuilder.Build(builder, featureContext);
                builder.Populate(mvcBuilder.Services);
            });
        }

        private static FeatureContext CreateFeatureContext(IContainer shellContainer)
        {
            var builder = shellContainer.Resolve<IFeatureContextBuilder>();
            return builder.Build();
        }

        private static void MergeAppParts(IMvcBuilder mvcBuilder, FeatureContext context)
        {
            foreach (var item in context.Items.SelectMany(x => x.Items.Select(a => a.Assembly)))
            {
                mvcBuilder.AddApplicationPart(item).AddControllersAsServices();
            }
        }
    }
}
