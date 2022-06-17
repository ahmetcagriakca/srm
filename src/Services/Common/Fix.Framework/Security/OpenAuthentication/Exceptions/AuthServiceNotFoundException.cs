using Fix.Exceptions;
using System;

namespace Fix.Security.OpenAuthentication.Exceptions
{
    public class AuthServiceNotFoundException : FixException
    {
        public AuthServiceNotFoundException(string message) : base(message)
        {
        }

        public AuthServiceNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
