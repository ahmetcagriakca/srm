using Autofac;
using Fix.Environment.FeatureManagement;
using Fix.Environment.FileSystem;
using Fix.Environment.Shell;
using Fix.Jobs.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System.Linq;
using System.Reflection;

namespace Fix.Jobs
{
    public class QuartzStartup
    {
        public void StartJobs(IApplicationBuilder app, QuartzConfig config)
        {
            if (config.IsValid(out string message))
            {
                var scheduler = app.ApplicationServices.GetService<IScheduler>();
                var typeFinder = app.ApplicationServices.GetService<ITypeFinder>();

                //TODO:feature check
                ContainerBuilder containerBuilder = new ContainerBuilder();
                IFeatureContextBuilder builder = ShellScopeBuilder.Instance.Build(containerBuilder).Resolve<IFeatureContextBuilder>();
                var context = builder.Build();
                //typeFinder.FindClassesOf(assemblies, GetPredicate(typeof(IJob)));
                foreach (var job in config.Jobs)
                {
                    if (job.IsEnabled)
                    {
                        foreach (var assemblyItem in context.Items.SelectMany(x => x.Items).Distinct(new AssemblyItemEquality()))
                        {
                            var jobClass = assemblyItem.DependencyContext.Jobs.FirstOrDefault(en => en.FullName == job.JobName);
                            if (jobClass != null)
                            {
                                MethodInfo method = typeof(QuartzStartup).GetMethod("StartJob");
                                MethodInfo genericMethod = method.MakeGenericMethod(jobClass);
                                object[] objects = new object[2];
                                objects[0] = scheduler;
                                objects[1] = job.JobCrone;
                                genericMethod.Invoke(this, objects);
                            }
                        }
                    }
                }
            }
        }
        public void StartJob<TJob>(IScheduler scheduler, string cron)
            where TJob : IJob
        {

            var jobName = typeof(TJob).FullName;

            var job = JobBuilder.Create<TJob>()
                .WithIdentity(jobName)
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity($"{jobName}.trigger")
                .StartNow()
                .WithCronSchedule(cron)
                //.WithSimpleSchedule(scheduleBuilder =>
                //    scheduleBuilder
                //        .WithIntervalInSeconds(runInterval)
                //        .RepeatForever())
                .Build();

            scheduler.ScheduleJob(job, trigger);
        }
    }
}
