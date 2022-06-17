using Autofac;
using Fix.Data;
using Fix.Environment.Aspects;
using Fix.Environment.FeatureManagement;
using Fix.Environment.FileSystem;
using Fix.Environment.Shell;
using IdentityServer.Infrastructor;
using Microsoft.AspNetCore.Http;
using System;

namespace IdentityServer.UnitTest.Facades
{
    internal sealed class TestScopeBuilder
    {
        private TestScopeBuilder()
        {

        }

        private static readonly object locker = new object();
        static TestScopeBuilder instance;

        public static TestScopeBuilder Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (locker)
                    {
                        if (instance == null)
                            instance = new TestScopeBuilder();
                    }
                }

                return instance;
            }
        }

        public IContainer Build(Action<ContainerBuilder> registration = null)
        {
            var builder = new ContainerBuilder();
            {
                //builder.RegisterType<TaskManager>().As<ITaskManager>();
                //builder.RegisterType<WorkContext>().As<IWorkContext>();
                //builder.RegisterType<TransactionManager>().As<ITransactionManager>();
                //     builder.RegisterType<SimpleCacheManager>().As<ICacheManager>();

                builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>();
                builder.RegisterType<ShellContextBuilder>().As<IDepedencyContextBuilder>();
                builder.RegisterType<WorkScopeBuilder>().As<IWorkScopeBuilder>();
                builder.RegisterType<TypeFinder>().As<ITypeFinder>();
                builder.RegisterType<DefaultAssemblyLoader>().As<IAssemblyLoader>();
                builder.RegisterType<AspectPolicyBuilder>().As<IAspectPolicyBuilder>();

                //Features management
                {
                    builder.RegisterType<FeatureContextBuilder>().As<IFeatureContextBuilder>().SingleInstance();
                    builder.RegisterType<FeatureDirectoryService>().As<IFeatureDirectoryService>().SingleInstance();
                    builder.RegisterType<FeaturePathProvider>().As<IFeaturePathProvider>().SingleInstance();
                    builder.RegisterType<FeatureProvider>().As<IFeatureProvider>().SingleInstance();
                }

                builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));
                if (registration != null)
                {
                    registration.Invoke(builder);
                }
            }

            return builder.Build();
        }
        public ContainerBuilder Build()
        {
            var builder = new ContainerBuilder();
            {
                //builder.RegisterType<TaskManager>().As<ITaskManager>();
                //builder.RegisterType<WorkContext>().As<IWorkContext>();
                //builder.RegisterType<TransactionManager>().As<ITransactionManager>();
                //     builder.RegisterType<SimpleCacheManager>().As<ICacheManager>();

                builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>();
                builder.RegisterType<ShellContextBuilder>().As<IDepedencyContextBuilder>();
                builder.RegisterType<WorkScopeBuilder>().As<IWorkScopeBuilder>();
                builder.RegisterType<TypeFinder>().As<ITypeFinder>();
                builder.RegisterType<DefaultAssemblyLoader>().As<IAssemblyLoader>();
                builder.RegisterType<AspectPolicyBuilder>().As<IAspectPolicyBuilder>();

                //Features management
                {
                    builder.RegisterType<FeatureContextBuilder>().As<IFeatureContextBuilder>().SingleInstance();
                    builder.RegisterType<FeatureDirectoryService>().As<IFeatureDirectoryService>().SingleInstance();
                    builder.RegisterType<FeaturePathProvider>().As<IFeaturePathProvider>().SingleInstance();
                    builder.RegisterType<FeatureProvider>().As<IFeatureProvider>().SingleInstance();
                }

                builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));
            }

            return builder;
        }
    }
}
