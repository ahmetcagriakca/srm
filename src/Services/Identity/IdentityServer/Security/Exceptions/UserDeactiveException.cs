using Fix.Exceptions;
using System;

namespace IdentityServer.Security.Exceptions
{
    public class UserDeactiveException : FixException
    {
        public UserDeactiveException(string message) : base(message)
        {

        }
        public UserDeactiveException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
