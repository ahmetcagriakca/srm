using IdentityServer.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.UnitTest.Facades
{
    public class DataInitializer : IDataInitializer
    {
        public void InitializeData(DbContext dbContext)
        {


            var company = new Company() { Id = 1, IsDelete = false, IsActive = true, Name = "Company1", SystemName = "Test" };
            dbContext.Set<Company>().Add(company);
            dbContext.SaveChanges();

            dbContext.Set<Role>().Add(new Role() { Id = 1, IsDelete = false, IsActive = true, Name = "Administrator", Description = "admin" });
            dbContext.Set<Role>().Add(new Role() { Id = 2, IsDelete = false, IsActive = true, Name = "Instructor", Description = "Instructor" });
            dbContext.Set<Role>().Add(new Role() { Id = 3, IsDelete = false, IsActive = true, Name = "Driver", Description = "Driver" });
            dbContext.Set<Role>().Add(new Role() { Id = 4, IsDelete = false, IsActive = true, Name = "Secretary", Description = "Secretary" });
            dbContext.SaveChanges();
            var user1 = new User() { Id = 1, IsDelete = false, IsActive = true, CompanyId = 1, UserName = "admin", Password = "21232f297a57a5a743894a0e4a801fc3", Name = "admin", Surname = "admin", Email = "test@test", MobilePhone = "5555555555" };
            var user2 = new User() { Id = 2, IsDelete = false, IsActive = true, CompanyId = 1, UserName = "ogretmen1", Password = "21232f297a57a5a743894a0e4a801fc3", Name = "ogretmen1", Surname = "ogretmen1", Email = "test@test", MobilePhone = "5555555555" };
            var user3 = new User() { Id = 3, IsDelete = false, IsActive = true, CompanyId = 1, UserName = "ogretmen2", Password = "21232f297a57a5a743894a0e4a801fc3", Name = "ogretmen2", Surname = "ogretmen2", Email = "test@test", MobilePhone = "5555555555" };
            var user4 = new User() { Id = 4, IsDelete = false, IsActive = true, CompanyId = 1, UserName = "34ACA34", Password = "21232f297a57a5a743894a0e4a801fc3", Name = "şoför", Surname = "şoför", Email = "test@test", MobilePhone = "5555555555" };
            var user5 = new User() { Id = 5, IsDelete = false, IsActive = true, CompanyId = 1, UserName = "sekreter", Password = "21232f297a57a5a743894a0e4a801fc3", Name = "sekreter", Surname = "sekreter", Email = "test@test", MobilePhone = "5555555555" };
            dbContext.Set<User>().Add(user1);
            dbContext.Set<User>().Add(user2);
            dbContext.Set<User>().Add(user3);
            dbContext.Set<User>().Add(user4);
            dbContext.Set<User>().Add(user5);
            dbContext.SaveChanges();

            dbContext.Set<UserInRole>().Add(new UserInRole() { Id = 1, IsDelete = false, IsActive = true, UserId = 1, RoleId = 1 });
            dbContext.Set<UserInRole>().Add(new UserInRole() { Id = 2, IsDelete = false, IsActive = true, UserId = 2, RoleId = 2 });
            dbContext.Set<UserInRole>().Add(new UserInRole() { Id = 3, IsDelete = false, IsActive = true, UserId = 3, RoleId = 2 });
            dbContext.Set<UserInRole>().Add(new UserInRole() { Id = 4, IsDelete = false, IsActive = true, UserId = 4, RoleId = 3 });
            dbContext.Set<UserInRole>().Add(new UserInRole() { Id = 5, IsDelete = false, IsActive = true, UserId = 5, RoleId = 4 });
            dbContext.SaveChanges();
            dbContext.Set<RolePermission>().Add(new RolePermission() { Id = 4, IsDelete = false, IsActive = true, Permission = "TEST", });
            dbContext.SaveChanges();
        }
    }
}
