using Fix.Exceptions;
using System;

namespace IdentityServer.Security.Exceptions
{
    public class UserNotFoundException : FixException
    {
        public UserNotFoundException(string message) : base(message)
        {

        }
        public UserNotFoundException(string message, Exception innerException) : base(message, innerException)
        {

        }

    }
}
