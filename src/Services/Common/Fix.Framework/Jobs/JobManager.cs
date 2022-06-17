using Quartz;
using System;
using System.Collections.Generic;

namespace Fix.Jobs
{
    public class JobKeyValue
    {
        public string Key { get; set; }
        public object Value { get; set; }
    }
    public class JobManager : IJobManager
    {
        private readonly IScheduler scheduler;

        public JobManager(IScheduler scheduler)
        {
            this.scheduler = scheduler;
        }
        public void Run<TJob>(List<JobKeyValue> jobData = null, int runInterval = 0, int repeatCount = 0)
            where TJob : IJob
        {
            StartJob<TJob>(scheduler, jobData, runInterval, repeatCount);
            //MethodInfo method = typeof(JobManager).GetMethod("StartJob");
            //MethodInfo genericMethod = method.MakeGenericMethod(jobClass);
            //object[] objects = new object[2];
            //objects[0] = scheduler;
            //objects[1] = 5;
            //                    genericMethod.Invoke(this, objects);
        }
        private void StartJob<TJob>(IScheduler scheduler, List<JobKeyValue> jobData = null, int runInterval = 0, int repeatCount = 0)
            where TJob : IJob
        {
            var jobName = typeof(TJob).FullName;

            var job = JobBuilder.Create<TJob>()
                .WithIdentity(jobName)
                .Build();
            if (jobData?.Count > 0)
            {
                foreach (var item in jobData)
                {
                    job.JobDataMap.Put(item.Key, item.Value);
                }
            }
            ITrigger trigger;
            if (runInterval == 0)
            {

                trigger = TriggerBuilder.Create()
                   .WithIdentity($"{jobName}.trigger")
                   .StartNow()
                   .Build();
            }
            else
            {
                if (repeatCount == 0)
                {
                    trigger = TriggerBuilder.Create()
                    .WithIdentity($"{jobName}.trigger")
                    .StartAt(DateTime.Now.AddSeconds(runInterval))
                    .Build();
                }
                else
                {
                    trigger = TriggerBuilder.Create()
                    .WithIdentity($"{jobName}.trigger")
                    .StartAt(DateTime.Now.AddSeconds(runInterval))
                    .WithSimpleSchedule(scheduleBuilder =>
                        scheduleBuilder
                            .WithIntervalInSeconds(runInterval)
                            .WithRepeatCount(repeatCount))

                    .Build();
                }
            }

            scheduler.ScheduleJob(job, trigger);
        }
    }
}
