using Fix.Exceptions;

namespace Fix.Security.OpenAuthentication.Exceptions
{
    public class TokenNotFoundException : FixException
    {
        public TokenNotFoundException(string message) : base(message)
        {

        }
    }
}
