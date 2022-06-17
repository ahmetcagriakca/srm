using Fix.Environment.Extensions;
using Fix.Environment.FileSystem;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Quartz;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Fix.Environment.Shell
{
    public class ShellContextBuilder : IDepedencyContextBuilder
    {
        private readonly ITypeFinder typeFinder;

        public ShellContextBuilder(ITypeFinder typeFinder)
        {
            this.typeFinder = typeFinder ?? throw new ArgumentNullException(nameof(typeFinder));
        }

        public DependencyContext Build(Assembly assembly)
        {
            IEnumerable<Assembly> assemblies = new List<Assembly> { assembly };
            return Build(assemblies);
        }
        public DependencyContext Build(IEnumerable<Assembly> assemblies)
        {
            return new DependencyContext
            {
                Validators = typeFinder.FindClassesOf(assemblies, AssignableFrom(typeof(IValidator<>))),
                Services = typeFinder.FindClassesOf(assemblies, GetPredicate(typeof(IDependency))),
                Generics = typeFinder.FindClassesOf(assemblies, AssignableFrom(typeof(IDependency))),
                Controllers = typeFinder.FindClassesOf(assemblies, GetPredicate(typeof(Controller))),
                Jobs = typeFinder.FindClassesOf(assemblies, GetPredicate(typeof(IJob))),
            };
        }

        public Func<Type, bool> GetPredicate(Type type)
        {
            return t => t.GetTypeInfo().IsClass && !t.GetTypeInfo().IsAbstract && !t.IsGenericType && type.IsAssignableFrom(t);
        }

        public Func<Type, bool> GetGenericPredicate(Type type)
        {
            return t => t.GetTypeInfo().IsClass && !t.GetTypeInfo().IsAbstract && t.IsGenericType && type.IsAssignableFrom(t);
        }

        public Func<Type, bool> AssignableFrom(Type type)
        {
            if (type.IsGenericType)
            {

                return t => t.IsAssignableToGenericType(type);
            }
            else
                return t => t.GetTypeInfo().IsClass && !t.GetTypeInfo().IsAbstract && t.IsGenericType && type.IsAssignableFrom(t);
        }

    }
}
