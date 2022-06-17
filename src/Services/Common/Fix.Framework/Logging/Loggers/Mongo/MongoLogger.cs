using Fix.Data.Mongo;
using Humanizer;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace Fix.Logging.Loggers.Mongo
{
    public class MongoLogger : BaseLogger
    {
        private static ReaderWriterLockSlim _readWriteLock = new ReaderWriterLockSlim();

        private readonly IMongoDbContextLocator contextLocator;
        protected static IMongoCollection<LogData> Collection;
        protected static string collectionName = typeof(LogData).Name.Pluralize().ToLower();
        public MongoLogger(
            ILogDataFactory logDataFactory,
            IMongoDbContextLocator contextLocator = null
            ) : base(logDataFactory)
        {
            if (contextLocator != null)
            {
                this.contextLocator = contextLocator;
                if (this.contextLocator.Current != null)
                {

                    Collection = contextLocator.Current.Database.GetCollection<LogData>(collectionName);
                }
            }
        }

        public override string AliasName => "MongoLogger";
        protected override void LogInternal<T>(LogData<T> logData)
        {
            LogInternalAsync(logData).Wait();
        }

        protected override async Task LogInternalAsync<T>(LogData<T> logData)
        {
            if (contextLocator != null)
            {
                await Collection.InsertOneAsync(logData);
            }
        }
    }
}
