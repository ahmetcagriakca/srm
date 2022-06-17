using System;
using System.Threading.Tasks;
using Serilog;
using Serilog.Events;

namespace Fix.Exceptions.Handlers
{
    public class FileLoggerExceptionHandler : IExceptionHandler
    {
        public FileLoggerExceptionHandler()
        {
        }

        public void Handle(Exception exception)
        {
            Log.Write(LogEventLevel.Error, exception, "Exception Handled logs");
        }

        public async Task HandleAsync(Exception exception)
        {
            await Task.Run(() => Handle(exception));
        }
    }
}
