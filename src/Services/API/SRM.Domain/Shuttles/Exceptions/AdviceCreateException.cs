using Fix.Exceptions;

namespace SRM.Domain.Shuttles.Exceptions
{
    public class AdviceCreateException : FixException
    {
        public AdviceCreateException(string message) : base(message)
        {

        }

    }

}
