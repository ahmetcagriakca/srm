using Fix.Data;
using Fix.Exceptions;
using Fix.Logging;
using Quartz;
using System;
using System.Threading.Tasks;

namespace Fix.Jobs
{
    public abstract class BaseJob : IJob
    {
        protected readonly ILogManager logManager;
        private readonly IExceptionManager exceptionManager;
        private readonly ITransactionManager transactionManager;

        public BaseJob(
            ILogManager logManager,
            IExceptionManager exceptionManager,
            ITransactionManager transactionManager
            )
        {
            this.logManager = logManager;
            this.exceptionManager = exceptionManager;
            this.transactionManager = transactionManager;
        }
        public virtual Task Execute(IJobExecutionContext context)
        {
            return Task.Run(() => ExecuteAsync(context));
        }

        public virtual async Task ExecuteAsync(IJobExecutionContext context)
        {
            try
            {
                await Start(context);
            }
            catch (Exception ex)
            {
                StaticLogger.WriteToFile("Getting Error on execute Start method. Error" + ex);
            }
            try
            {
                JobAction(context);
                transactionManager.Commit();
            }
            catch (Exception ex)
            {
                try
                {
                    await exceptionManager.HandleAsync(ex);
                }
                catch (Exception exEx)
                {
                    StaticLogger.WriteToFile("Getting Error on execute JobAction method. Error" + exEx);
                }
                finally
                {
                    await transactionManager.RollbackAsync();
                }
            }
            try
            {
                await Finish(context);
            }
            catch (Exception ex)
            {
                StaticLogger.WriteToFile("Getting Error on execute Finish method. Error" + ex);
            }
        }
        public virtual async Task Start(IJobExecutionContext context)
        {
            await logManager.Logger.TraceAsync($"{context.JobDetail.Key} started at {DateTime.Now}.");
        }
        public virtual async Task Finish(IJobExecutionContext context)
        {
            await logManager.Logger.TraceAsync($"{context.JobDetail.Key} finished at {DateTime.Now}.");
        }
        public abstract void JobAction(IJobExecutionContext context);
    }
}
