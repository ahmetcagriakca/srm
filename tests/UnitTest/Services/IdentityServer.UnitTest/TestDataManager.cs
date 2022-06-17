using Fix.Security;
using IdentityServer.Models;
using IdentityServer.Security;
using System.Linq;

namespace IdentityServer.UnitTest
{
    public class TestDataManager
    {
        public TestContainerManager ContainerManager;
        public TestDataManager(TestContainerManager containerManager)
        {
            CreateBaseData(containerManager);
        }

        public void CreateBaseData(TestContainerManager containerManager)
        {
            this.ContainerManager = containerManager;
        }


        public void CreateUser(ISecurityDomain securityDomain, User user)
        {
            var permissionProvider = ContainerManager.Resolve<IPermissionProvider>();
            var role = new Role
            {
                Name = "System Administrator",
                IsActive = true,
                Permissions = permissionProvider.GetPermissions().Select(x => new RolePermission()
                {
                    Permission = x
                }).ToList(),
            };
            user.UserInRoles.Add(new UserInRole
            {
                Role = role
            });
            securityDomain.Account.CreateUser(user);
            ContainerManager.Commit();
        }
    }
}
