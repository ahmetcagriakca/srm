using Fix.Data;
using Fix.Security;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SRM.Data
{

    public class Repository<T> : IRepository<T> where T : class, ITraceable, ICorporable, new()
    {
        protected readonly IAuthenticationProvider _authenticationProvider;
        protected readonly DbContext Context;
        public Repository(
            IDbContextLocator contextLocator,
            IAuthenticationProvider authenticationProvider)
        {
            if (contextLocator == null)
            {
                throw new ArgumentNullException(nameof(contextLocator));
            }

            _authenticationProvider = authenticationProvider;
            Context = contextLocator.Current;
        }

        /// <summary>
        /// Table base object getting all table data with some restriction
        /// CompanyId came from authentication and all values will adding with Corporation Id 
        /// </summary>
        public virtual IQueryable<T> Table
        {
            get
            {
                return Context.Set<T>().Where(en => en.CompanyId == _authenticationProvider.GetCompanyId());
            }
        }

        /// <summary>
        /// Getting table with any restriction 
        /// </summary>
        /// <param name="includes">Include Relational objects</param>
        /// <returns></returns>
        public IQueryable<T> GetAllWithoutRestriction(params Expression<Func<T, object>>[] includes)
        {
            var result = Context.Set<T>() as IQueryable<T>;
            if (includes.Any())
            {
                result = includes.Aggregate(result, (current, include) => current.Include(include));
            }
            return result;
        }

        public IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            var result = Table;
            if (includes.Any())
            {
                result = includes.Aggregate(result, (current, include) => current.Include(include));
            }
            return result;
        }

        public T GetBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var result = GetAll(includes);
            return result.FirstOrDefault(predicate);
        }

        public T FindBy(object id)
        {
            return Context.Set<T>().Find(id);
        }

        public void Add(T entity)
        {
            Context.Set<T>().Add(entity);
        }

        public async Task AddAsync(T entity)
        {
            await Context.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            Context.Set<T>().Attach(entity);
            Context.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            Context.Set<T>().Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }
        public IQueryable<T> Fetch(Expression<Func<T, bool>> predicate)
        {
            return Table.Where(predicate);
        }

        public IQueryable<T> Fetch(Expression<Func<T, bool>> predicate, Action<Orderable<T>> order)
        {
            var orderable = new Orderable<T>(Fetch(predicate));
            order(orderable);
            return orderable.Queryable;
        }
        public IQueryable<T> Fetch(Expression<Func<T, bool>> predicate, Action<Orderable<T>> order, int skip, int count)
        {
            return Fetch(predicate, order).Skip(skip).Take(count);
        }

        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return Table.Any(predicate);
        }

        public async Task<IEnumerable<T>> FetchAsync(Expression<Func<T, bool>> predicate)
        {
            return await Fetch(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> FetchAsync(Expression<Func<T, bool>> predicate, Action<Orderable<T>> order)
        {
            return await Fetch(predicate, order).ToListAsync();
        }

        public async Task<IEnumerable<T>> FetchAsync(Expression<Func<T, bool>> predicate, Action<Orderable<T>> order, int skip, int count)
        {
            return await Fetch(predicate, order, skip, count).ToListAsync();
        }
    }
}
