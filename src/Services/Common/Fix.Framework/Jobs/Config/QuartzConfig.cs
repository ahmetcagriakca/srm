using System.Collections.Generic;

namespace Fix.Jobs.Config
{
    public class QuartzConfig
    {
        public bool UseQuartz { get; set; }
        public List<JobConfig> Jobs { get; set; }
        public bool IsValid()
        {
            bool valid = true;
            if (!(Jobs?.Count > 0))
            {
                valid = false;
            }
            return valid;
        }

        public bool IsValid(out string message)
        {
            bool valid = true;
            message = string.Empty;
            if (!(Jobs?.Count > 0))
            {
                message = "Jobs not defined";
                valid = false;
            }

            return valid;
        }
    }
}
