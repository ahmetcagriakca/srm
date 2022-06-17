using Fix.Data;
using SRM.Data.Models.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SRM.Domain.Parameters.Services
{
    public class ApplicationParameterService : IApplicationParameterService
    {
        protected readonly IRepository<ApplicationParameter> _repository;
        public ApplicationParameterService(IRepository<ApplicationParameter> repository)
        {
            _repository = repository;
        }

        public virtual ApplicationParameter GetById(int id)
        {
            return _repository.FindBy(id);
        }

        public virtual IQueryable<ApplicationParameter> Get()
        {
            return _repository.GetAllWithoutRestriction();
        }

        public virtual IEnumerable<ApplicationParameter> Search(Expression<Func<ApplicationParameter, bool>> predicate)
        {
            var result = Get().Where(predicate);

            return result;
        }

        public virtual void Create(ApplicationParameter entity)
        {
            _repository.Add(entity);
        }

        public virtual void Update(ApplicationParameter entity)
        {
            _repository.Update(entity);
        }

        public virtual void Delete(int id)
        {
            var entity = GetById(id);
            _repository.Delete(entity);
        }

        public IEnumerable<ApplicationParameter> GetListByName(string name)
        {
            return _repository.GetAllWithoutRestriction().Where(en => en.Name == name);
        }

        public ApplicationParameter GetByName(string name)
        {
            return _repository.GetAllWithoutRestriction().FirstOrDefault(en => en.Name == name);
        }

        public bool HasParameter(string name)
        {
            return _repository.GetAllWithoutRestriction().Any(en => en.Name == name);
        }
    }
}
