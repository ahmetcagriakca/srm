using System;

namespace Fix.Exceptions
{
    public abstract class FixException : Exception
    {
        public FixException(string message) : base(message) { }
        public FixException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class FatalException : FixException
    {
        public FatalException(string message) : base(message) { }
        public FatalException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class ShellException : FatalException
    {
        public ShellException(string message) : base(message) { }
        public ShellException(string message, Exception innerException) : base(message, innerException) { }
    }
    public class OperationalException : FixException
    {
        public OperationalException(string message) : base(message) { }
        public OperationalException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class ValidationException : OperationalException
    {
        public ValidationException(string message) : base(message) { }
        public ValidationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
