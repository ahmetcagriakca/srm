using Fix.Data;
using Fix.Security;
using Fix.Security.Cryptography;
using Fix.Utility;
using IdentityServer.Models;
using IdentityServer.Security.Exceptions;
using IdentityServer.Security.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IdentityServer.Security.Services
{
    public class AccountService : IAccountService, IAuthorizer
    {
        private readonly ICryptoService cryptoService;
        private readonly IRepository<User> userRepository;
        private readonly IRepository<Role> roleRepository;
        private readonly IRepository<UserInRole> userInRoleRepository;
        private readonly IRepository<UserSession> userSessionRepository;
        private readonly IEnumerable<IPermissionProvider> permissionProviders;
        private readonly IAuthenticationProvider authenticationProvider;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cryptoService"></param>
        /// <param name="userRepository"></param>
        /// <param name="userInRoleRepository"></param>
        /// <param name="userSessionRepository"></param>
        /// <param name="roleRepository"></param>
        /// <param name="permissionProviders"></param>
        /// <param name="authenticationProvider"></param>
        public AccountService(
            ICryptoService cryptoService,
            IRepository<User> userRepository,
            IRepository<UserInRole> userInRoleRepository,
            IRepository<UserSession> userSessionRepository,
            IRepository<Role> roleRepository,
            IEnumerable<IPermissionProvider> permissionProviders,
            IAuthenticationProvider authenticationProvider
            )
        {
            this.cryptoService = cryptoService ?? throw new ArgumentNullException(nameof(cryptoService));
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            this.roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            this.userInRoleRepository = userInRoleRepository ?? throw new ArgumentNullException(nameof(userInRoleRepository));
            this.userSessionRepository = userSessionRepository ?? throw new ArgumentNullException(nameof(userSessionRepository));
            this.permissionProviders = permissionProviders ?? throw new ArgumentNullException(nameof(permissionProviders));
            this.authenticationProvider = authenticationProvider;
        }

        public bool IsUserExist(string username)
        {
            return userRepository.Any(x => x.UserName == username);
        }
        public bool CanSignIn(string username, string password)
        {
            password = cryptoService.Encrypt(password, HashTypes.MD5).Value;
            return userRepository.GetAllWithoutRestriction().Any(u => u.UserName == username && u.Password == password && u.IsActive);
        }

        public User GetUser(string username)
        {
            var user = userRepository.GetAllWithoutRestriction().FirstOrDefault(x => x.UserName == username);
            if (user == null)
                throw new UserNotFoundException("The specified username(" + username + ") not found.");

            return user;
        }

        public User GetUser(string username, string password)
        {
            var user = GetUserWithRelations(username);
            if (user.Password != cryptoService.Encrypt(password, HashTypes.MD5).Value)
                throw new PasswordMismatchException("wrong password");

            return user;
        }

        public User GetUser(long userId)
        {
            var user = GetUserWithRelations(userId);
            return user;
        }

        public IQueryable<User> GetUsersByRoles(string roleName)
        {
            var users = userRepository.GetAllWithoutRestriction()
                .Include(l => l.UserSessions)
                .Include(u => u.UserInRoles)
                .ThenInclude(r => r.Role)
                .Include(u => u.Company)
                .Where(en => en.UserInRoles.Any(enr => enr.Role.Name == roleName) && en.IsActive);
            return users;
        }

        public void Register(User user)
        {
            if (IsUserExist(user.UserName))
            {
                throw new UserAlreadyExistsException("Kullanıcı adı mevcut");
            }

            user.IsActive = false;
            user.Password = cryptoService.Encrypt(user.Password, HashTypes.MD5).Value;
            userRepository.Add(user);
        }

        public void Activate(long userId)
        {
            var user = GetUser(userId);
            if (user == null)
                throw new UserNotFoundException("The specified user id(" + userId + ") not found");

            userRepository.Update(user);
        }

        public PagedList<User> GetUsers(int pageIndex)
        {
            return new PagedList<User>(userRepository.GetAllWithoutRestriction(), pageIndex);
        }
        public User GetUserWithRelations(long userId)
        {
            var user = userRepository.GetAllWithoutRestriction()
                .Include(l => l.UserSessions)
                .Include(u => u.UserInRoles)
                .ThenInclude(r => r.Role)
                .ThenInclude(r => r.Permissions)
                .Include(u => u.Company)
                .FirstOrDefault(x => x.Id == userId);
            if (user == null)
                throw new UserNotFoundException("The specified user Id(" + userId + ") not found");

            return user;
        }
        public User GetUserWithRelations(string username)
        {
            var user = userRepository.GetAllWithoutRestriction()
                .Include(l => l.UserSessions)
                .Include(u => u.UserInRoles)
                .ThenInclude(r => r.Role)
                .ThenInclude(r => r.Permissions)
                .Include(u => u.Company)
                .FirstOrDefault(x => x.UserName == username);
            if (user == null)
                throw new UserNotFoundException("The specified user Id(" + username + ") not found");

            return user;
        }

        public void CreateUser(User user)
        {
            if (!IsUserExist(user.UserName))
            {
                user.Password = cryptoService.Encrypt(user.Password, HashTypes.MD5).Value;
                userRepository.Add(user);
            }
        }
        public void UpdateUser(User user)
        {
            userRepository.Update(user);
        }

        /// <summary>
        /// Checking UserName is exists if UserName is exists added number to end of the username 
        /// </summary>
        /// <param name="userName">unique user name</param>
        /// <returns></returns>
        public string CreateUniqueUserName(string userName)
        {
            var userNameAlreadyExist = false;
            int userCount = 1;
            string userCountString = string.Empty;
            string lastUserName;
            do
            {
                if (userNameAlreadyExist)
                {
                    userCountString = (++userCount).ToString();
                }
                lastUserName = userName + userCountString;
                userNameAlreadyExist = IsUserExist(userName);
            } while (userNameAlreadyExist);
            return lastUserName;
        }

        public void AddUserSession(UserSession userSession)
        {
            userSessionRepository.Add(userSession);
        }

        public IEnumerable<string> GetPermissions()
        {
            return permissionProviders.SelectMany(x => x.GetPermissions());
        }

        public IEnumerable<string> UserPermissions(string userName)
        {
            return GetUserWithRelations(userName)
                .UserInRoles
                .SelectMany(x => x.Role.Permissions.Select(p => p.Permission));
        }

        public bool IsRoleExist(string name)
        {
            return roleRepository.GetAllWithoutRestriction().Any(x => x.Name == name);
        }
        public bool IsRoleExist(int id)
        {
            return roleRepository.GetAllWithoutRestriction().Any(x => x.Id == id);
        }
        public void AddRole(Role role)
        {
            if (IsRoleExist(role.Name))
            {
                throw new RoleExistsException("Role exist");
            }

            var availablePermissions = GetPermissions();
            if (role.Permissions.Select(x => x.Permission).Except(availablePermissions).Any())
            {
                throw new UnKnowPermissionException("One or more permission is undefined");
            }
            roleRepository.Add(role);
        }
        public void SaveRole(Role role)
        {
            if (IsRoleExist(role.Name))
            {
                throw new RoleExistsException("Role exist.");
            }

            var availablePermissions = GetPermissions();
            if (role.Permissions.Select(x => x.Permission).Except(availablePermissions).Any())
            {
                throw new UnKnowPermissionException("One or more permission is undefined");
            }
            roleRepository.Update(role);
        }
        public Role GetRole(int id)
        {
            var role = roleRepository.GetAllWithoutRestriction().FirstOrDefault(x => x.Id == id);

            if (role == null)
                throw new RoleNotFoundException("The specified role id{" + id + "} not found.");

            return role;
        }

        public Role GetRoleByName(string name)
        {
            var role = roleRepository.GetAllWithoutRestriction().FirstOrDefault(x => x.Name == name);

            if (role == null)
                throw new RoleNotFoundException("The specified role name{" + name + "} not found.");

            return role;
        }

        public Role GetRoleWithRelations(int id)
        {
            var role = roleRepository.GetAllWithoutRestriction()
                .Include(x => x.Permissions)
                .FirstOrDefault(x => x.Id == id);

            if (role == null)
                throw new RoleNotFoundException($"The specified role id({id}) not found.");

            return role;
        }
        public IEnumerable<Role> GetRoles()
        {
            return roleRepository.GetAllWithoutRestriction().Where(en => en.IsActive);
        }

        public bool Authorize(string permission)
        {
            var userName = authenticationProvider.GetUserName();
            var permissions = UserPermissions(userName);
            return permissions.Contains(permission);
        }

        public void AddUserRole(long userId, int roleId)
        {

            var user = GetUser(userId);
            var role = GetRole(roleId);
            if (!userInRoleRepository.Any(en => en.User.Id == userId && en.Role.Id == roleId))
                user.UserInRoles.Add(new UserInRole() { Role = role });

        }

        public void AddUserRole(User user, int roleId)
        {

            var role = GetRole(roleId);
            userInRoleRepository.Add(new UserInRole() { User = user, Role = role });
            //if (!userInRoleRepository.Any(en => en.User.Id == userId && en.Role.Id == roleId))
            //    user.UserInRoles.Add(new UserInRole() { Role = role });

        }

        public void ChangePassword(long userId, string oldPassword, string newPassword)
        {
            var user = GetUser(userId);
            if (!user.IsActive)
                throw new UserDeactiveException("User state is disable.");

            if (user.Password != cryptoService.Encrypt(oldPassword, HashTypes.MD5).Value)
            {
                throw new PasswordMismatchException("Password dit not match.");
            }
            user.Password = cryptoService.Encrypt(newPassword, HashTypes.MD5).Value;
            userRepository.Update(user);
        }

        public ClientContext CreateUserContext(User user)
        {
            var permissions = UserPermissions(user.UserName);

            return new ClientContext
            {
                Key = user.UserName,
                Permissions = permissions.ToList(),
                UserId = user.Id
            };
        }
        public bool TryGetUserContext(string username, string password, out ClientContext context, out User user)
        {
            context = null;
            user = GetUser(username, password);
            if (!user.IsActive)
                return false;

            var permissions = user.UserInRoles.Select(x => x.Role)
                .SelectMany(x => x.Permissions)
                .Select(x => x.Permission);

            context = new ClientContext
            {
                Key = user.UserName,
                Permissions = permissions.ToList(),
                UserId = user.Id
            };
            return true;
        }

        public bool TryGetUserContext(long userId, out ClientContext context, out User user)
        {
            context = null;
            user = GetUser(userId);
            if (!user.IsActive)
                return false;

            var permissions = user.UserInRoles.Select(x => x.Role)
                .SelectMany(x => x.Permissions)
                .Select(x => x.Permission);

            context = new ClientContext
            {
                Key = user.UserName,
                Permissions = permissions.ToList(),
                UserId = user.Id
            };
            return true;
        }
    }
}
