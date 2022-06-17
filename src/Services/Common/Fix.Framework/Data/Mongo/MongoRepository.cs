using Fix.Data.Exceptions;
using Fix.Data.Mongo;
using Humanizer;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fix.Data
{
    public class MongoRepository<T> : IMongoRepository<T> where T : IEntity
    {
        private readonly IMongoRepositoryEventHandler<T> repositoryEventHandler;
        protected static IMongoCollection<T> Collection;
        public virtual IMongoCollection<T> GetCollection => Collection;

        protected static string collectionName = typeof(T).Name.Pluralize().ToLower();
        protected HttpContext httpContext;
        private IMongoDbContextLocator _contextLocator;

        public MongoRepository(
            IMongoDbContextLocator contextLocator,
            IMongoRepositoryEventHandler<T> repositoryEventHandler
            )
        {
            _contextLocator = contextLocator;
            this.repositoryEventHandler = repositoryEventHandler;
            Collection = _contextLocator.Current.Database.GetCollection<T>(collectionName);
        }

        public virtual T GetById(object id)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            var list = GetCollection.Find(filter)
                .ToList();
            return list.FirstOrDefault();
        }

        public virtual IEnumerable<T> GetByIds<TE>(IEnumerable<TE> idList)
        {
            if (idList?.Count() > 0)
            {
                foreach (var id in idList)
                {
                    yield return GetById(id);
                }
            }
            else
            {
                yield break;
            }
        }

        public virtual async Task<T> GetByIdAsync(object id)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            var list = await GetCollection.Find(filter)
                .ToListAsync();
            return list.FirstOrDefault();
        }

        public virtual IQueryable<T> All()
        {
            return GetCollection.AsQueryable();
        }

        public virtual async Task<IQueryable<T>> AllAsync()
        {
            var result = await GetCollection.AsQueryable().ToListAsync();
            return result.AsQueryable();
        }

        public virtual IQueryable<T> Find(FilterDefinition<T> filter)
        {
            return GetCollection.Find(filter).ToEnumerable().AsQueryable();
        }
        public virtual async Task<IQueryable<T>> FindAsync(FilterDefinition<T> filter)
        {
            var result = await GetCollection.Find(filter).ToListAsync();
            return result.AsQueryable();
        }
        public async Task<IQueryable<TValue>> GetFieldValues<TValue>(FilterDefinition<T> filter, Expression<Func<T, TValue>> fieldExpression)
        {
            var propertyValue = await GetCollection
                .Find(filter)
                .Project(new ProjectionDefinitionBuilder<T>().Expression(fieldExpression)).ToListAsync()
                ;

            return propertyValue.AsQueryable();
        }

        public virtual T Add(T entity)
        {
            entity = AddAsync(entity).Result;
            return entity;
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            repositoryEventHandler.OnInserting(entity);
            await GetCollection.InsertOneAsync(entity);
            return entity;
        }

        public virtual ReplaceOneResult ReplaceOne(T entity)
        {
            var result = ReplaceOneAsync(entity).Result;
            return result;
        }

        public virtual async Task<ReplaceOneResult> ReplaceOneAsync(T entity)
        {
            object id = null;
            if (entity.GetType().GetProperty("Id") != null)
                id = entity.GetType().GetProperty("Id").GetValue(entity, null);
            if (id != null)
            {
                repositoryEventHandler.OnModifying(entity);
                var filter = Builders<T>.Filter.Eq("_id", id);
                var result = await GetCollection.ReplaceOneAsync(filter, entity);
                return result;
            }
            else
            {
                throw new MongoEntityNotFoundException("Kayda ait id alanı bulunamadı.");
            }
        }

        public DeleteResult Delete(T entity)
        {
            var result = DeleteAsync(entity).Result;
            return result;
        }

        public async Task<DeleteResult> DeleteAsync(T entity)
        {
            object id = null;
            if (entity.GetType().GetProperty("Id") != null)
                id = entity.GetType().GetProperty("Id").GetValue(entity, null);
            if (id != null)
            {
                var filter = Builders<T>.Filter.Eq("_id", id);

                var result = await GetCollection.DeleteOneAsync(filter);
                return result;
            }
            else
            {
                throw new MongoEntityNotFoundException("Kayda ait id alanı bulunamadı.");
            }
        }

        public virtual DeleteResult DeleteMany(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            var result = GetCollection.DeleteMany(expression);
            return result;
        }

        public virtual async Task<DeleteResult> DeleteManyAsync(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            var result = await GetCollection.DeleteManyAsync(expression);
            return result;
        }


    }
}
