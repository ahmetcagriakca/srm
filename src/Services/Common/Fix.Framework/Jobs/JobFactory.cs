using Fix.Jobs.Config;
using Quartz;
using Quartz.Spi;
using System;

namespace Fix.Jobs
{
    public class JobFactory : IJobFactory
    {
        private readonly IServiceProvider serviceProvider;
        private readonly QuartzConfig config;

        public JobFactory(IServiceProvider serviceProvider, QuartzConfig config)
        {
            this.serviceProvider = serviceProvider;
            this.config = config;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            if (config.UseQuartz)
            {
                var jobDetail = bundle.JobDetail;

                var job = (IJob)serviceProvider.GetService(jobDetail.JobType);
                return job;
            }
            else
            {
                return default;
            }
            //return Container.GetService(bundle.JobDetail.JobType) as IJob;
        }

        public void ReturnJob(IJob job)
        {
            (job as IDisposable)?.Dispose();
        }
    }
}
