using Fix.Exceptions;

namespace IdentityServer.Security.Exceptions
{
    public class PasswordMismatchException : FixException
    {
        public PasswordMismatchException(string message) : base(message)
        {

        }
    }
}
