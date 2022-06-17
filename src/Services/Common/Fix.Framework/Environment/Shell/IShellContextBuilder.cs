using System.Collections.Generic;
using System.Reflection;

namespace Fix.Environment.Shell
{
    public interface IDepedencyContextBuilder
    {
        DependencyContext Build(IEnumerable<Assembly> assemblies);
        DependencyContext Build(Assembly assembly);
    }
}
