using System;
using System.Collections.Generic;
using System.Reflection;

namespace Fix.Environment.FileSystem
{
    public interface ITypeFinder
    {
        IEnumerable<Type> FindClassesOf<T>(IEnumerable<Assembly> assemblies);
        IEnumerable<Type> FindClassesOf(IEnumerable<Assembly> assemblies, Type type);
        IEnumerable<Type> FindClassesOf(IEnumerable<Assembly> assemblies, Func<Type, bool> predicate);
    }
}
