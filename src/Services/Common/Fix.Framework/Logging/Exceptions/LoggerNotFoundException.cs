using Fix.Exceptions;

namespace Fix.Logging.Exceptions
{
    public class LoggerNotFoundException : FixException
    {
        public LoggerNotFoundException(string message) : base(message)
        {

        }
    }
}
