using Fix.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SRM.Domain.Parameters.Services
{
    public abstract class ParameterService<T> : IParameterService<T> where T : IEntity, new()
    {
        protected readonly IRepository<T> _repository;
        public ParameterService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual T GetById(int id)
        {
            return _repository.FindBy(id);
        }

        public virtual IQueryable<T> Get()
        {
            return _repository.Table;
        }

        public virtual IEnumerable<T> Search(Expression<Func<T, bool>> predicate)
        {
            var result = Get().Where(predicate);

            return result;
        }

        public virtual void Create(T entity)
        {
            _repository.Add(entity);
        }

        public virtual void Update(T entity)
        {
            _repository.Update(entity);
        }

        public virtual void Delete(int id)
        {
            var entity = GetById(id);
            _repository.Delete(entity);
        }
    }
}
