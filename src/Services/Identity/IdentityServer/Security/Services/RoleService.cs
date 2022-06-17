using Fix;
using Fix.Data;
using IdentityServer.Models;
using System.Collections.Generic;
using System.Linq;

namespace IdentityServer.Security.Services
{
    public class RoleService : IRoleService
    {
        internal readonly IRepository<Role> repository;
        public RoleService(IRepository<Role> repository)
        {
            this.repository = repository;
        }

        public Role GetById(int id)
        {
            var entity = repository.FindBy(id);
            return entity;
        }

        public IQueryable<Role> Get()
        {
            var entities = repository.Table;
            return entities;
        }
        public IEnumerable<Role> Search(long? id, string name, string description, bool? isActive)
        {
            var result = repository.Table
                .Where(en => (id == null || en.Id == id)
                && (name.IsNullOrEmpty() || en.Name.ToUpper().Contains(name.ToUpper()))
                && (description.IsNullOrEmpty() || en.Description.ToUpper().Contains(description.ToUpper()))
                             && (isActive == null || en.IsActive == isActive)
                ).OrderBy(en => en.Id);
            return result;
        }

        public Role Create(Role entity)
        {
            repository.Add(entity);
            return entity;
        }

        public void Update(Role entity)
        {
            repository.Update(entity);
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            repository.Delete(entity);
        }

    }
}
