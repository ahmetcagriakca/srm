using Fix.Exceptions;
using System;

namespace IdentityServer.Security.Exceptions
{
    public class RoleExistsException : FixException
    {
        public RoleExistsException(string message) : base(message)
        {

        }
        public RoleExistsException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
