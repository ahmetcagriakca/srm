using Fix.Exceptions;
using System;

namespace IdentityServer.Security.Exceptions.StudentException
{
    public class StudentNotFoundException : FixException
    {

        public StudentNotFoundException(string message) : base(message)
        {

        }
        public StudentNotFoundException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
