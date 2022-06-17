using Fix.Exceptions;
using System;

namespace Fix.Logging.Exceptions
{
    public class InvalidLogLevelException : OperationalException
    {
        public InvalidLogLevelException(string message) : base(message)
        {

        }

        public InvalidLogLevelException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
