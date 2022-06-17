using System.Collections.Generic;
using System.IO;

namespace Fix.Environment.FeatureManagement
{
    public class Feature
    {
        public string FriendlyName { get; set; }
        public List<FileInfo> Files { get; set; }
    }
}
