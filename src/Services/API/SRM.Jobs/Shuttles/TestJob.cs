using Fix.Data;
using Fix.Exceptions;
using Fix.Jobs;
using Fix.Logging;
using Quartz;
using System.Threading;

namespace SRM.Jobs.Shuttles
{
    public class TestJob : BaseJob
    {
        public TestJob(ILogManager logManager, IExceptionManager exceptionManager, ITransactionManager transactionManager) : base(logManager, exceptionManager, transactionManager)
        {
        }

        public override void JobAction(IJobExecutionContext context)
        {
            Thread.Sleep(5000);
            logManager.Logger.Trace("JobEvent completed");
        }
    }
}
