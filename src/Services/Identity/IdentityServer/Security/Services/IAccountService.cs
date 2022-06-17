using Fix;
using Fix.Utility;
using IdentityServer.Models;
using IdentityServer.Security.Models;
using System.Collections.Generic;
using System.Linq;

namespace IdentityServer.Security.Services
{
    public interface IAccountService : IDependency
    {
        bool IsUserExist(string username);
        void CreateUser(User user);
        void UpdateUser(User user);

        /// <summary>
        /// Checking Username is exists if Username is exists added number to end of the username 
        /// </summary>
        /// <param name="userName">unique user name</param>
        /// <returns></returns>
        string CreateUniqueUserName(string userName);

        User GetUser(long userId);
        User GetUserWithRelations(long userId);
        User GetUser(string username);
        User GetUser(string username, string password);
        IQueryable<User> GetUsersByRoles(string roleName);
        bool CanSignIn(string username, string password);
        PagedList<User> GetUsers(int pageIndex);
        void Register(User user);
        void Activate(long userId);
        void AddRole(Role role);
        void SaveRole(Role role);
        Role GetRole(int id);
        Role GetRoleByName(string name);
        Role GetRoleWithRelations(int id);
        IEnumerable<Role> GetRoles();
        bool IsRoleExist(string name);
        IEnumerable<string> GetPermissions();
        IEnumerable<string> UserPermissions(string userName);
        void ChangePassword(long userId, string oldPassword, string newPassword);
        ClientContext CreateUserContext(User user);
        bool TryGetUserContext(string username, string password, out ClientContext context, out User user);
        bool TryGetUserContext(long userId, out ClientContext context, out User user);
        void AddUserRole(long userId, int roleId);
        void AddUserRole(User user, int roleId);
    }
}
