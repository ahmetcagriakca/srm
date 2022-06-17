using Fix.Exceptions;
using System;

namespace Fix.Logging.Exceptions
{
    public class InvalidLogIteratorNameException : OperationalException
    {
        public InvalidLogIteratorNameException(string message) : base(message)
        {
        }

        public InvalidLogIteratorNameException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
