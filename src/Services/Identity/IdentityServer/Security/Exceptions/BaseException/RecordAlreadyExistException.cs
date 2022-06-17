using Fix.Exceptions;
using System;

namespace IdentityServer.Security.Exceptions.BaseException
{
    public class RecordAlreadyExistException : FixException
    {
        public RecordAlreadyExistException(string message) : base(message)
        {

        }
        public RecordAlreadyExistException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }

}
