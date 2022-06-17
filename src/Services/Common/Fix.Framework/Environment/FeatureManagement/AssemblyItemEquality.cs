using System.Collections.Generic;

namespace Fix.Environment.FeatureManagement
{
    public class AssemblyItemEquality : IEqualityComparer<AssemblyItem>
    {
        public bool Equals(AssemblyItem x, AssemblyItem y)
        {
            return (x.Assembly.FullName == y.Assembly.FullName);
        }

        public int GetHashCode(AssemblyItem obj)
        {
            return 0;
        }
    }
}
