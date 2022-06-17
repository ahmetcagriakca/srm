using Fix.Data.Mongo;
using Humanizer;
using MongoDB.Driver;
using System.Linq;

namespace Fix.Data
{
    public class MongoRepositorySequence<T> : IMongoRepositorySequence<T>
    {
        protected IMongoCollection<MongoSequence> sequenceCollection;
        protected string ReferenceCollectionName = typeof(T).Name.Pluralize().ToLower();
        public MongoRepositorySequence(IMongoDbContextLocator contextLocator)
        {
            sequenceCollection = contextLocator.Current.Database.GetCollection<MongoSequence>("sequences");
            var sequence = sequenceCollection.Find(x => x.Id == ReferenceCollectionName).ToList().FirstOrDefault();
            if (sequence == null)
            {
                sequenceCollection.InsertOne(new MongoSequence() { Id = ReferenceCollectionName, Sequence = 0 });
            }
        }

        public virtual object GetNextSequence()
        {
            var update = Builders<MongoSequence>.Update
                .Inc("Sequence", 1);
            var filter = Builders<MongoSequence>.Filter.Eq("_id", ReferenceCollectionName);
            _ = sequenceCollection.UpdateOne(filter, update);
            return GetSequence(ReferenceCollectionName);
        }

        private object GetSequence(string name)
        {
            var sequence = sequenceCollection.Find(x => x.Id == name).ToList().FirstOrDefault();
            return sequence.Sequence;
        }
    }
}
