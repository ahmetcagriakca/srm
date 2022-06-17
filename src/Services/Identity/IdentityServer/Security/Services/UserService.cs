using Fix.Data;
using IdentityServer.Models;
using System.Linq;

namespace IdentityServer.Security.Services
{
    public class UserService : IUserService
    {
        internal readonly IRepository<User> repository;
        public UserService(IRepository<User> repository)
        {
            this.repository = repository;
        }

        public User GetById(int id)
        {
            var entity = repository.FindBy(id);
            return entity;
        }

        public IQueryable<User> Get()
        {
            var entities = repository.Table;
            return entities;
        }

        public User Create(User entity)
        {
            repository.Add(entity);
            return entity;
        }

        public void Update(User entity)
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
