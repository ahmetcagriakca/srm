namespace Fix.Logging.Loggers.Elasticsearch
{
    //public class LoggingIndex : BaseIndex
    //{
    //    private readonly ElasticsearchConfig config;

    //    public LoggingIndex(ElasticsearchConfig config)
    //    {
    //        this.config = config ?? throw new ArgumentNullException(nameof(config));
    //    }
    //    public override string Name => config.IndexName;
    //    protected override ElasticClient CreateClient()
    //    {
    //        var node = new SingleNodeConnectionPool(new Uri(config.Url));
    //        var settings = new ConnectionSettings(node);
    //        return new ElasticClient(settings);
    //    }

    //    protected override void CreateIndex()
    //    {
    //        var response = Client.CreateIndex(Name, c => c
    //        .Mappings(m => m.Map<LogDocType>("log", x => x.AutoMap()))
    //        .Settings(s => s
    //            .NumberOfShards(1)
    //            .NumberOfReplicas(0))
    //        );
    //    }
    //}
}
