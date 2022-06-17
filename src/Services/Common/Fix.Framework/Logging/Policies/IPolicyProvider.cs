using System;

namespace Fix.Logging.Policies
{
    public interface IPolicyProvider : IScoped
    {
        T Get<T>() where T : BasePolicy;
        object Get(Type type);
    }
}
