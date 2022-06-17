using Fix.Jobs.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;

namespace Fix.Jobs.Extensions
{
    public static class QuartzExtensions
    {
        public static QuartzConfig GetQuartzConfig(this IConfiguration configuration, string name)
        {
            var section = configuration.GetSection(name);
            var config = new QuartzConfig();
            section.Bind(config);

            return config;
        }

        public static IServiceCollection UseQuartz(this IServiceCollection services, QuartzConfig config, params Type[] jobs)
        {
            if (config.IsValid(out string message))
            {
                services.AddSingleton(config);
                services.AddSingleton<IJobFactory, JobFactory>();

                return services.AddSingleton(provider =>
                 {
                     var schedulerFactory = new StdSchedulerFactory();
                     var scheduler = schedulerFactory.GetScheduler().Result;
                     scheduler.JobFactory = provider.GetService<IJobFactory>();
                     scheduler.Start();
                     return scheduler;
                 });
            }
            return services;
        }

        public static void StartJob<TJob>(IScheduler scheduler, int runInterval)
            where TJob : IJob
        {
            var jobName = typeof(TJob).FullName;

            var job = JobBuilder.Create<TJob>()
                .WithIdentity(jobName)
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity($"{jobName}.trigger")
                .StartNow()
                .WithSimpleSchedule(scheduleBuilder =>
                    scheduleBuilder
                        .WithIntervalInSeconds(runInterval)
                        .RepeatForever())
                .Build();

            scheduler.ScheduleJob(job, trigger);
        }
    }
}
