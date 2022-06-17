using System;
using System.Runtime.Serialization;

namespace Fix.Configuration.Exceptions
{
    public class ConfigurationNotValidException : Exception
    {
        public ConfigurationNotValidException()
        {
        }

        public ConfigurationNotValidException(string message) : base(message)
        {
        }

        public ConfigurationNotValidException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ConfigurationNotValidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
