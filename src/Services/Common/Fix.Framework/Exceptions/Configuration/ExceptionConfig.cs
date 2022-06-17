using Fix.Configuration;
using System.Collections.Generic;

namespace Fix.Exceptions.Configuration
{
    public class ExceptionConfig : IConfigurationBase
    {
        public ExceptionConfig()
        {
            Policies = new List<PolicyConfig>();
        }
        public IList<PolicyConfig> Policies { get; set; }

        public Dictionary<string, string> HandlerTypes { get; set; }

        public bool IsValid()
        {
            return true;
        }

        public bool IsValid(out string message)
        {
            message = null;
            return true;
        }
    }
}
