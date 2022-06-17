using Fix;
using IdentityServer.Models;
using System.Linq;

namespace IdentityServer.Security.Services
{
    public interface IUserService : IDependency
    {
        IQueryable<User> Get();
        User GetById(int id);
        User Create(User entity);
        void Update(User entity);
        void Delete(int id);
    }
}
