using System.Collections.Generic;

namespace Fix.Exceptions.Configuration
{
    public class PolicyConfig
    {
        public PolicyConfig()
        {
            HandlerAlias = new List<string>();
        }
        public string ExceptionType { get; set; }
        public string IteratorType { get; set; }
        public IList<string> HandlerAlias { get; set; }
        public string IsEnabled { get; set; }

    }
}
