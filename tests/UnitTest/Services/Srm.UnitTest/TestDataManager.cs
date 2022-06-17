//using SRM.Data.Models.Accounts;

namespace Srm.UnitTest
{
    //public class TestDataManager
    //{
    //    public TestContainerManager ContainerManager ;
    //    public TestDataManager(TestContainerManager containerManager )
    //    {
    //        CreateBaseData(containerManager);
    //    }

    //    public void CreateBaseData(TestContainerManager containerManager)
    //    {
    //        this.ContainerManager = containerManager;
    //        var securityDomain = ContainerManager.Resolve<ISecurityDomain>();
    //        if (securityDomain.Account.IsUserExist("admin")) return;

    //        var user = new User()
    //        {
    //            Username = "admin",
    //            Password = "admin",
    //            Name = "admin",
    //            Surname = "admin",
    //            IsActive = true
    //        };
    //        CreateUser(securityDomain, user);

    //        var instructorRole = new Role
    //        {
    //            Name = "Instructor",
    //            Description = "Öğretmen",
    //            IsActive = true,
    //        };
    //        securityDomain.Account.AddRole(instructorRole);
    //        ContainerManager.Commit();
    //    }


    //    public void CreateUser(ISecurityDomain securityDomain, User user)
    //    {
    //        var permissionProvider = ContainerManager.Resolve<IPermissionProvider>();
    //        var role = new Role
    //        {
    //            Name = "System Administrator",
    //            IsActive = true,
    //            Permissions = permissionProvider.GetPermissions().Select(x => new RolePermission()
    //            {
    //                Permission = x
    //            }).ToList(),
    //        };
    //        user.UserInRoles.Add(new UserInRole
    //        {
    //            Role = role
    //        });
    //        securityDomain.Account.CreateUser(user);
    //        ContainerManager.Commit();
    //    }

    //    //// laziness + thread safety
    //    //private static Lazy<TestDataManager> instance =
    //    //    new Lazy<TestDataManager>(() => new TestDataManager());

    //    //public static TestDataManager Instance => instance.Value;
    //}
}
