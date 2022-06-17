using System;

namespace Fix.Exceptions.Policy
{
    public interface IPolicyProvider : ISingleton
    {
        Policy Get(Exception exception);
    }
}
