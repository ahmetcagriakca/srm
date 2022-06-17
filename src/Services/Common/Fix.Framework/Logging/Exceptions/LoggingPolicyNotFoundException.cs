using Fix.Exceptions;
using System;

namespace Fix.Logging.Exceptions
{
    public class LoggingPolicyNotFoundException : FixException
    {
        public LoggingPolicyNotFoundException(string message) : base(message)
        {
        }

        public LoggingPolicyNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
