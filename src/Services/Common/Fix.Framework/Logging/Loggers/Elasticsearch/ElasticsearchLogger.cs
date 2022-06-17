namespace Fix.Logging.Loggers.Elasticsearch
{
    //public class ElasticsearchLogger : BaseLogger, IElasticsearchLogger
    //{
    //    private const string ALIAS_NAME = "ElasticsearchLogger";
    //    private readonly IElasticService<LoggingIndex> elasticService;

    //    public override string AliasName => ALIAS_NAME;

    //    public ElasticsearchLogger(IElasticService<LoggingIndex> elasticService, ILogDataFactory logDataFactory) : base(logDataFactory)
    //    {
    //        this.elasticService = elasticService ?? throw new ArgumentNullException(nameof(elasticService));
    //    }

    //    protected override async Task LogInternalAsync<T>(LogData<T> logData)
    //    {
    //        var doc = CreateLogDoc(logData);
    //        await PutLogAsync(doc);
    //    }

    //    protected override void LogInternal<T>(LogData<T> logData)
    //    {
    //        var doc = CreateLogDoc(logData);
    //        PutLog(doc);
    //    }
    //    private LogDocType CreateLogDoc<T>(LogData<T> logData) where T : class
    //    {
    //        return new LogDocType()
    //        {
    //            Id = Guid.NewGuid(),
    //            CorrelationId = logData.CorrlId,
    //            CurrentUser = logData.User,
    //            MachineName = logData.MachineName,
    //            Timestamp = logData.UtcTimestamp,
    //            LoggingObject = JsonConvert.SerializeObject(logData.TLogInstance),
    //            AppName = logData.AppName,
    //            IsHttpApp = true,
    //            Message = logData.Message,
    //            Level = logData.LogLevel.ToString(),
    //        };
    //    }

    //    public void PutLog(LogDocType docType)
    //    {
    //        var response = elasticService.Insert(docType);
    //    }

    //    public async Task PutLogAsync(LogDocType docType)
    //    {
    //        await elasticService.InsertAsync(docType);
    //    }
    //}
}