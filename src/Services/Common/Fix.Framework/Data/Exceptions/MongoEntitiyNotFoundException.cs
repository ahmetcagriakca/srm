using Fix.Exceptions;
using System;

namespace Fix.Data.Exceptions
{
    public class MongoEntityNotFoundException : FixException
    {
        public MongoEntityNotFoundException(string message) : base(message)
        {
        }

        public MongoEntityNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
