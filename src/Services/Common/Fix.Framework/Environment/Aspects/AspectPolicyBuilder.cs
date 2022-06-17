using System.Collections.Generic;

namespace Fix.Environment.Aspects
{
    public sealed class AspectPolicyBuilder : IAspectPolicyBuilder
    {
        private static List<AspectPolicy> policiesHolder = new List<AspectPolicy>();

        public AspectPolicyBuilder Use(AspectPolicy policy)
        {
            policiesHolder.Add(policy);
            return this;
        }

        public AspectPolicyBuilder Use(IEnumerable<AspectPolicy> policies)
        {
            policiesHolder.AddRange(policies);
            return this;
        }
    }
}
