using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Fix.Environment.FileSystem
{
    public class TypeFinder : ITypeFinder
    {
        public IEnumerable<Type> FindClassesOf<T>(IEnumerable<Assembly> assemblies)
        {
            return FindClassesOf(assemblies, (typeof(T)));
        }

        public IEnumerable<Type> FindClassesOf(IEnumerable<Assembly> assemblies, Type type)
        {
            return FindClassesOf(assemblies, x => type.IsAssignableFrom(x.GetType()));
        }

        public IEnumerable<Type> FindClassesOf(IEnumerable<Assembly> assemblies, Func<Type, bool> predicate)
        {
            return assemblies.SelectMany(x => x.GetExportedTypes().Where(predicate));
        }
    }

}
