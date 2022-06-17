using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fix.Data
{
    public interface IRepository<T> : IScoped where T : IEntity, new()
    {

        /// <summary>
        /// Table base object getting all table data with some restriction
        /// CompanyId came from authentication and all values will adding with Corporation Id 
        /// </summary>
        IQueryable<T> Table { get; }

        /// <summary>
        /// Getting table with any restriction 
        /// </summary>
        /// <param name="includes">Include Relational objects</param>
        /// <returns></returns>
        IQueryable<T> GetAllWithoutRestriction(params Expression<Func<T, object>>[] includes);
        IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes);
        T GetBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        T FindBy(object id);
        void Add(T entity);
        Task AddAsync(T entity);
        void Delete(T entity);
        void Update(T entity);

        bool Any(Expression<Func<T, bool>> predicate);
        IQueryable<T> Fetch(Expression<Func<T, bool>> predicate);
        IQueryable<T> Fetch(Expression<Func<T, bool>> predicate, Action<Orderable<T>> order);
        IQueryable<T> Fetch(Expression<Func<T, bool>> predicate, Action<Orderable<T>> order, int skip, int count);
        Task<IEnumerable<T>> FetchAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> FetchAsync(Expression<Func<T, bool>> predicate, Action<Orderable<T>> order);
        Task<IEnumerable<T>> FetchAsync(Expression<Func<T, bool>> predicate, Action<Orderable<T>> order, int skip, int count);
    }
}
