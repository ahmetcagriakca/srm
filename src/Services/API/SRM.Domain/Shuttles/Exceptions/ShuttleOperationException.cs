using Fix.Exceptions;

namespace SRM.Domain.Shuttles.Exceptions
{
    public class ShuttleOperationException : FixException
    {
        public ShuttleOperationException(string message) : base(message)
        {

        }
    }
}