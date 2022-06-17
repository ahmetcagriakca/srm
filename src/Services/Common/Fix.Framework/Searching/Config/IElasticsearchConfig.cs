using System.Collections.Generic;

namespace Fix.Searching.Config
{
    public interface IElasticsearchConfig
    {
        IAuthenticationSection Authentication { get; set; }
        IConnectionPoolSection ConnectionPool { get; set; }
        string DefaultIndex { get; set; }
    }
    public interface IAuthenticationSection
    {
        bool UseAuthentication { get; set; }
    }

    public interface IConnectionPoolSection
    {
        bool UseSingleNode { get; set; }
        List<string> Urls { get; set; }
    }

}
