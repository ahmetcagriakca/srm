namespace Fix.Data.Mongo
{
    public interface IMongoDbContextLocator : IScoped
    {
        MongoDbContext Current { get; }
    }
}
