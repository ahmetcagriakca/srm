using Fix.Exceptions;

namespace IdentityServer.Security.Exceptions
{
    public class UnKnowPermissionException : FixException
    {
        public UnKnowPermissionException(string message) : base(message)
        {

        }
    }
}
