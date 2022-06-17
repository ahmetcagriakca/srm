using System;
using System.Collections.Generic;

namespace Fix.Environment.Shell
{
    public class DependencyContext
    {
        public IEnumerable<Type> Services { get; set; }
        public IEnumerable<Type> Generics { get; set; }
        public IEnumerable<Type> Controllers { get; set; }
        public IEnumerable<Type> Validators { get; set; }
        public IEnumerable<Type> Jobs { get; set; }
    }
}
