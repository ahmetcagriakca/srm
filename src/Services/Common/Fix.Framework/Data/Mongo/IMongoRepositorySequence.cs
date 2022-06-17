namespace Fix.Data
{
    public interface IMongoRepositorySequence<T> : IScoped
    {
        object GetNextSequence();
    }
}