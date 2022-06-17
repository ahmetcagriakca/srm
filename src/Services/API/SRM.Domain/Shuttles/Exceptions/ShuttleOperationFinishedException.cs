using Fix.Exceptions;

namespace SRM.Domain.Shuttles.Exceptions
{
    public class ShuttleOperationFinishedException : FixException
    {
        public ShuttleOperationFinishedException(string message) : base(message)
        {

        }
    }
}
