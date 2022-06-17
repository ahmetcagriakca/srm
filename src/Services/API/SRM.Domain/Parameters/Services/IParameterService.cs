using Fix;
using Fix.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SRM.Domain.Parameters.Services
{
    public interface IParameterService<T> : IDependency where T : IEntity, new()
    {
        IQueryable<T> Get();
        IEnumerable<T> Search(Expression<Func<T, bool>> predicate);
        T GetById(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
