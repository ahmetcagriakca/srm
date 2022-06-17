using System;

namespace Fix.Environment.Dependency
{
    public interface IDependencySolver : IDependency
    {
        T Get<T>();
        object Get(Type type);
    }
}
