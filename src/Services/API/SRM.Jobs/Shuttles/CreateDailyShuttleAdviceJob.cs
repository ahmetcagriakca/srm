using Fix.Data;
using Fix.Exceptions;
using Fix.Jobs;
using Fix.Logging;
using Quartz;
using SRM.Domain.Shuttles.OperationManagement.Services;
using System;

namespace SRM.Jobs.Shuttles
{
    public class CreateDailyShuttleAdviceJob : BaseJob
    {
        private readonly IShuttleService shuttleService;

        public CreateDailyShuttleAdviceJob(ILogManager logManager, IExceptionManager exceptionManager, ITransactionManager transactionManager,
            IShuttleService shuttleService) : base(logManager, exceptionManager, transactionManager)
        {
            this.shuttleService = shuttleService;
        }

        public override void JobAction(IJobExecutionContext context)
        {
            shuttleService.CreateAdvice(DateTime.Now.Date, 0);
        }
    }
}
