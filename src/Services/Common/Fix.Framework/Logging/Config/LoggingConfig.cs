using Fix.Configuration;
using System.Collections.Generic;

namespace Fix.Logging.Config
{
    public class LoggingConfig : IConfigurationBase
    {
        public LoggingConfig()
        {
            Policies = new List<PolicyConfig>();
        }
        public List<PolicyConfig> Policies { get; set; }
        public FileLoggerConfig FileLoggerConfig { get; set; }
        public ElasticsearchConfig ElasticsearchConfig { get; set; }
        public bool IsValid()
        {
            bool valid = true;
            if (Policies.Count == 0) { }
            return valid;
        }

        public bool IsValid(out string message)
        {
            bool valid = true;
            message = string.Empty;
            if (Policies.Count == 0)
            {
                message = "Policies not defined";
                valid = false;
            }

            return valid;
        }
    }

    public class PolicyConfig
    {
        public PolicyConfig()
        {
            LoggerAlias = new List<string>();
            IsEnabled = "1";
        }
        public string IterationType { get; set; }
        public IList<string> LoggerAlias { get; set; }
        public string LogLevel { get; set; }
        public string IsEnabled { get; set; }
    }

    public class Logger
    {
        public string Alias { get; set; }

    }
}
