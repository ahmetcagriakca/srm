using Autofac;
using Fix.Environment.Aspects;
using Fix.Environment.FeatureManagement;
using Fix.Environment.FileSystem;
using Microsoft.AspNetCore.Http;
using System;

namespace Fix.Environment.Shell
{
    internal sealed class ShellScopeBuilder
    {
        private ShellScopeBuilder()
        {

        }

        private static readonly object locker = new object();
        static ShellScopeBuilder instance;

        public static ShellScopeBuilder Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (locker)
                    {
                        if (instance == null)
                            instance = new ShellScopeBuilder();
                    }
                }
                return instance;
            }
        }

        public IContainer Build(ContainerBuilder builder,Action<ContainerBuilder> registration = null)
        {
            {
                builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>();
                builder.RegisterType<ShellContextBuilder>().As<IDepedencyContextBuilder>();
                builder.RegisterType<WorkScopeBuilder>().As<IWorkScopeBuilder>();
                builder.RegisterType<TypeFinder>().As<ITypeFinder>();
                builder.RegisterType<DefaultAssemblyLoader>().As<IAssemblyLoader>();
                builder.RegisterType<AspectPolicyBuilder>().As<IAspectPolicyBuilder>();

                {
                    builder.RegisterType<FeatureContextBuilder>().As<IFeatureContextBuilder>().SingleInstance();
                    builder.RegisterType<FeatureDirectoryService>().As<IFeatureDirectoryService>().SingleInstance();
                    builder.RegisterType<FeaturePathProvider>().As<IFeaturePathProvider>().SingleInstance();
                    builder.RegisterType<FeatureProvider>().As<IFeatureProvider>().SingleInstance();
                }

                if (registration != null)
                {
                    registration.Invoke(builder);
                }
            }

            return builder.Build();
        }
    }
}
