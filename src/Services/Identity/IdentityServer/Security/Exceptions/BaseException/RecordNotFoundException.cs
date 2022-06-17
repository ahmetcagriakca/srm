using Fix.Exceptions;
using System;

namespace IdentityServer.Security.Exceptions.BaseException
{
    public class RecordNotFoundException : FixException
    {
        public RecordNotFoundException(string message) : base(message)
        {

        }
        public RecordNotFoundException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }

}
