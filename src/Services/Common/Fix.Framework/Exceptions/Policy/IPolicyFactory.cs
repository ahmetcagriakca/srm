using System;

namespace Fix.Exceptions.Policy
{
    public interface IPolicyFactory : ISingleton
    {
        Policy Create(Type exceptionType);
    }
}
