using Fix.Exceptions;
using System;

namespace IdentityServer.Security.Exceptions
{
    public class RoleNotFoundException : FixException
    {
        public RoleNotFoundException(string message) : base(message)
        {

        }
        public RoleNotFoundException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
