using Fix.Logging;
using Fix.Logging.Loggers.Mongo;
using System;
using System.Threading.Tasks;

namespace Fix.Exceptions.Handlers
{

    public class MongoExceptionHandler : IExceptionHandler
    {
        public object ExceptionId { get; private set; }
        private readonly ILogManager logManager;

        public MongoExceptionHandler(ILogManager logManager)
        {
            this.logManager = logManager ?? throw new ArgumentNullException(nameof(logManager));
        }


        public void Handle(Exception exception)
        {
            HandleAsync(exception).Wait();

        }

        public Task HandleAsync(Exception exception)
        {
            this.logManager.GetLogger<MongoLogger>().Error(exception);
            //ExceptionId = this.logManager.GetLogger<MongoLogger>().GetLastLogId();
            return Task.FromResult<object>(null);
        }
    }
}
