using Fix.Exceptions;

namespace SRM.Domain.Shuttles.Exceptions
{
    public class ShuttleOperationTemplateException : FixException
    {
        public ShuttleOperationTemplateException(string message) : base(message)
        {

        }
    }
}