namespace Fix.Data
{
    public interface IMongoRepositoryEventHandler<T> : IScoped
    {
        void OnInserting(T collection);
        void OnModifying(T collection);
        void OnDeleting(T collection);
    }
}
