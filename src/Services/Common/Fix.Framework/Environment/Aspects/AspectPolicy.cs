using System;

namespace Fix.Environment.Aspects
{
    public class AspectPolicy
    {
        public Func<Type, bool> Predicate { get; set; }
        public IInterceptor Interceptor { get; set; }
    }
}
