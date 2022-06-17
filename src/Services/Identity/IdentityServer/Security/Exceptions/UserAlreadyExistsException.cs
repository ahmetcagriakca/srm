using Fix.Exceptions;

namespace IdentityServer.Security.Exceptions
{
    public class UserAlreadyExistsException : FixException
    {
        public UserAlreadyExistsException(string message) : base(message)
        {

        }
    }
}
