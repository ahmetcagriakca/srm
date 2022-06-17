using Fix.Environment.Dependency;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fix.Logging.Policies
{
    public class PolicyPerformer : IPolicyPerformer
    {
        private readonly IDependencySolver dependencySolver;

        public PolicyPerformer(IDependencySolver dependencySolver)
        {
            this.dependencySolver = dependencySolver ?? throw new ArgumentNullException(nameof(dependencySolver));
        }

        public void Perform<T>(BasePolicy policy, LogData<T> logData) where T : class
        {
            policy.Iterator.Iterate(GetLoggers(policy), logData);
        }

        public async Task PerformAsync<T>(BasePolicy policy, LogData<T> logData) where T : class
        {
            await policy.Iterator.IterateAsync(GetLoggers(policy), logData);
        }

        private IEnumerable<ILogger> GetLoggers(BasePolicy policy)
        {
            foreach (var type in policy.GetTypes())
            {
                yield return (ILogger)dependencySolver.Get(type);
            }
        }
    }
}
