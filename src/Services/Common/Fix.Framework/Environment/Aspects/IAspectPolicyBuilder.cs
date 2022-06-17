using System.Collections.Generic;

namespace Fix.Environment.Aspects
{
    public interface IAspectPolicyBuilder
    {
        AspectPolicyBuilder Use(AspectPolicy policy);
        AspectPolicyBuilder Use(IEnumerable<AspectPolicy> policies);
    }
}
