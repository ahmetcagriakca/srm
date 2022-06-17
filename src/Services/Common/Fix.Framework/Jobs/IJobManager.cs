using Quartz;
using System.Collections.Generic;

namespace Fix.Jobs
{
    public interface IJobManager : IDependency
    {
        void Run<TJob>(List<JobKeyValue> jobData = null, int runInterval = 0, int repeatCount = 0) where TJob : IJob;
    }
}
