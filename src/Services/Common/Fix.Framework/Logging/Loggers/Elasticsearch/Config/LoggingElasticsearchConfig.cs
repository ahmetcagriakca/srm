using Fix.Searching.Config;
using System;

namespace Fix.Logging.Loggers.Elasticsearch.Config
{
    public class LoggingElasticsearchConfig : IElasticsearchConfig
    {
        public IAuthenticationSection Authentication { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IConnectionPoolSection ConnectionPool { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string DefaultIndex { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
