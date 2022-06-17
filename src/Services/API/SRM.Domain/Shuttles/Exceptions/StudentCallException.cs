using Fix.Exceptions;

namespace SRM.Domain.Shuttles.Exceptions
{
    public class StudentCallException : FixException
    {
        public StudentCallException(string message) : base(message)
        {

        }

    }
}