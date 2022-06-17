using Autofac;
using Autofac.Builder;
using Autofac.Extensions.DependencyInjection;
using Fix.Environment.FeatureManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Fix.Environment.Shell
{



    public class WorkScopeBuilder : IWorkScopeBuilder
    {
        private readonly IDepedencyContextBuilder contextBuilder;


        public WorkScopeBuilder(IDepedencyContextBuilder contextBuilder)
        {
            this.contextBuilder = contextBuilder ?? throw new ArgumentNullException(nameof(contextBuilder));
        }

        public void Build(ContainerBuilder builder, FeatureContext context)
        {

            foreach (var assemblyItem in context.Items.SelectMany(x => x.Items).Distinct(new AssemblyItemEquality()))
            {
                RegisterSelf(builder, assemblyItem.DependencyContext.Controllers);
                RegisterTypes(builder, assemblyItem.DependencyContext.Services);
                RegisterGenerics(builder, assemblyItem.DependencyContext.Generics);
                RegisterTypes(builder, assemblyItem.DependencyContext.Validators);
                RegisterJobs(builder, assemblyItem.DependencyContext.Jobs);
            }
        }

        /// <summary>
        /// Register job with singleton lifetime
        /// </summary>
        /// <param name="containerBuilder"></param>
        /// <param name="types"></param>
        private void RegisterJobs(ContainerBuilder containerBuilder, IEnumerable<Type> types)
        {
            containerBuilder.Populate(types.Select(jobType => new ServiceDescriptor(jobType, jobType, ServiceLifetime.Singleton)));
        }

        public ILifetimeScope Build(ILifetimeScope scope, FeatureContext context)
        {
            return null;
        }

        private void Register(ContainerBuilder containerBuilder, IEnumerable<Type> types)
        {
            foreach (var item in types)
            {
                if (item.IsGenericType)
                {

                }
                else
                {
                    containerBuilder.RegisterType(item);
                    foreach (var t in item.GetInterfaces())
                    {
                        if (t.IsGenericType)
                        {

                        }
                        else
                        {
                            containerBuilder.RegisterType(item).As(t);
                        }
                    }
                }
            }
        }


        private void RegisterSelf(ContainerBuilder builder, Type type)
        {
            builder.RegisterType(type).InstancePerDependency();
        }
        private void RegisterSelf(ContainerBuilder builder, IEnumerable<Type> types)
        {
            _ = types.Select(t => builder.RegisterType(t).As<Controller>().InstancePerDependency());
        }


        private void RegisterTypes(ContainerBuilder builder, IEnumerable<Type> types)
        {
            foreach (var implementer in types)
            {
                try
                {
                    RegisterInterface(builder, implementer);
                }
                catch (Exception ex)
                {
                    Debug.Write(ex);
                }
            }
        }


        private void RegisterInterface(ContainerBuilder builder, Type implementer)
        {
            builder.RegisterType(implementer).AsSelf();
            foreach (var serviceType in implementer.GetInterfaces())
            {
                var registration = builder.RegisterType(implementer).As(serviceType);
                SetLifetime(registration, serviceType);

            }
        }

        private void RegisterGenerics(ContainerBuilder builder, IEnumerable<Type> types)
        {
            foreach (var type in types)
            {
                RegisterGeneric(builder, type);
            }
        }


        private void RegisterGeneric(ContainerBuilder builder, Type type)
        {
            foreach (var service in type.GetInterfaces().Where(x => x.IsGenericType))
            {
                var registration = builder.RegisterGeneric(type).As(service);
                SetLifetime(registration, type);
            }
        }



        private void SetLifetime<TLimit, TActivatorData, TRegistrationStyle>(IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> registration, Type serviceType)
        {
            if (typeof(IScoped).IsAssignableFrom(serviceType))
            {
                registration.InstancePerLifetimeScope();
            }
            else if (typeof(ISingleton).IsAssignableFrom(serviceType))
            {
                registration.SingleInstance();
            }
            else registration.InstancePerDependency();
        }


    }
}
