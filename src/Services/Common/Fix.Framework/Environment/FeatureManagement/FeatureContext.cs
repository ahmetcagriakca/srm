using Fix.Environment.Shell;
using System.Collections.Generic;
using System.Reflection;

namespace Fix.Environment.FeatureManagement
{
    public class FeatureContext
    {
        public IEnumerable<FeatureContextItem> Items { get; set; }
    }

    public class FeatureContextItem
    {
        public FeatureContextItem(Feature feature)
        {
            Feature = feature;
        }
        public Feature Feature { get; }
        public IEnumerable<AssemblyItem> Items { get; set; }

    }

    public class AssemblyItem
    {
        public Assembly Assembly { get; set; }
        public DependencyContext DependencyContext { get; set; }
    }
}
