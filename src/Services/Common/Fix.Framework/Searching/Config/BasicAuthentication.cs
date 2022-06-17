using System.Collections.Generic;

namespace Fix.Searching.Config
{
    public class BasicAuthentication : IAuthenticationSection
    {
        public bool UseAuthentication { get; set; }
    }

    public class SingleNodeConnectionPool : IConnectionPoolSection
    {
        public SingleNodeConnectionPool()
        {
            Urls = new List<string>();
        }
        public bool UseSingleNode { get; set; }
        public List<string> Urls { get; set; }
    }
}
