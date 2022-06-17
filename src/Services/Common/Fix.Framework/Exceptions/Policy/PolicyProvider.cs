using System;
using System.Collections.Concurrent;

namespace Fix.Exceptions.Policy
{
    public class PolicyProvider : IPolicyProvider
    {
        public static ConcurrentDictionary<Type, Policy> policies = new ConcurrentDictionary<Type, Policy>();
        private static object locker = new object();
        private readonly IPolicyFactory policyFactory;

        public PolicyProvider(IPolicyFactory policyFactory)
        {
            policies = new ConcurrentDictionary<Type, Policy>();
            this.policyFactory = policyFactory ?? throw new ArgumentNullException(nameof(policyFactory));
        }

        public Policy Get(Exception exception)
        {
            var exceptionType = exception.GetType();
            if (!policies.TryGetValue(exceptionType, out Policy policy))
            {
                lock (locker)
                {
                    if (!policies.TryGetValue(exceptionType, out policy))
                    {
                        policy = policyFactory.Create(exceptionType);
                        policies.AddOrUpdate(exceptionType, policy, (x, y) => policy);
                    }
                }
            }
            return policy;
        }
    }
}
