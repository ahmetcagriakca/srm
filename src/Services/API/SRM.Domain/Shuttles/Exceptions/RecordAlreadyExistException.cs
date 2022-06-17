using Fix.Exceptions;
using System;

namespace SRM.Domain.Shuttles.OperationManagement.Services
{
    [Serializable]
    internal class RecordAlreadyExistException : FixException
    {
        public RecordAlreadyExistException(string message) : base(message)
        {
        }

        public RecordAlreadyExistException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}