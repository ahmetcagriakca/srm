using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fix.Data
{
    public interface IMongoRepository<T> : IScoped
        where T : IEntity
    {
        IMongoCollection<T> GetCollection { get; }
        IQueryable<T> All();
        IQueryable<T> Find(FilterDefinition<T> filter);
        T GetById(object id);
        IEnumerable<T> GetByIds<TE>(IEnumerable<TE> idList);
        Task<IQueryable<T>> AllAsync();
        Task<IQueryable<T>> FindAsync(FilterDefinition<T> filter);
        Task<T> GetByIdAsync(object id);
        Task<IQueryable<TValue>> GetFieldValues<TValue>(FilterDefinition<T> filter, Expression<Func<T, TValue>> fieldExpression);
        T Add(T entity);
        Task<T> AddAsync(T entity);
        ReplaceOneResult ReplaceOne(T entity);
        Task<ReplaceOneResult> ReplaceOneAsync(T entity);
        DeleteResult Delete(T entity);
        Task<DeleteResult> DeleteAsync(T entity);
        DeleteResult DeleteMany(Expression<Func<T, bool>> expression);
        Task<DeleteResult> DeleteManyAsync(Expression<Func<T, bool>> expression);
    }
}