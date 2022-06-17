using Fix;
using IdentityServer.Models;
using System.Collections.Generic;
using System.Linq;

namespace IdentityServer.Security.Services
{
    public interface IRoleService : IDependency
    {
        IQueryable<Role> Get();
        IEnumerable<Role> Search(long? id, string name, string description, bool? isActive);

        Role GetById(int id);
        Role Create(Role entity);
        void Update(Role entity);
        void Delete(int id);
    }
}
