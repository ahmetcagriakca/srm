using Fix.Exceptions;

namespace Fix.Security.OpenAuthentication.Exceptions
{
    public class AuthenticationTokenNotFoundException : FixException
    {
        public AuthenticationTokenNotFoundException(string message) : base(message)
        {
        }
    }
}
