using Fix.Exceptions;

namespace SRM.Domain.Shuttles.Exceptions
{
    public class StudentAvailableTimeExistException : FixException
    {
        public StudentAvailableTimeExistException(string message) : base(message)
        {

        }
    }
}
