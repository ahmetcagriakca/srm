using System.Collections.Generic;

namespace Fix.Logging.Config
{
    public class FileLoggerConfig
    {
        public string Directory { get; set; }
        public List<string> LoggingKeys { get; set; }
        public string DateFormat { get; set; }
    }
}
