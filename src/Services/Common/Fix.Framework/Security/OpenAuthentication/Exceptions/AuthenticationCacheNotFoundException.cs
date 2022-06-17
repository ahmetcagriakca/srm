using Fix.Exceptions;

namespace Fix.Security.OpenAuthentication.Exceptions
{
    public class AuthenticationCacheNotFoundException : FixException
    {
        public AuthenticationCacheNotFoundException(string message) : base(message)
        {
        }
    }
}
