using Elasticsearch.Net;

namespace Fix.Searching.Config
{
    public interface IConfigurationAdapter : IDependency
    {
        IConnectionPool ConnectionPoolType { get; set; }


    }
}
