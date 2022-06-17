using Fix.Environment.Dependency;
using System;

namespace Fix.Logging.Policies
{
    public class PolicyProvider : IPolicyProvider
    {
        private readonly IDependencySolver dependencySolver;
        public PolicyProvider(IDependencySolver dependencySolver)
        {
            this.dependencySolver = dependencySolver ?? throw new ArgumentNullException(nameof(dependencySolver));
        }

        public T Get<T>() where T : BasePolicy
        {
            return dependencySolver.Get<T>();
        }
        public object Get(Type type)
        {
            return dependencySolver.Get(type);
        }
    }
}
