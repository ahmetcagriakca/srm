using Fix.Exceptions.Iteration;
using System;
using System.Collections.Generic;

namespace Fix.Exceptions.Policy
{
    public class Policy
    {
        public IExceptionIterator Iterator { get; set; }
        public IEnumerable<Type> Types { get; set; }
    }
}
