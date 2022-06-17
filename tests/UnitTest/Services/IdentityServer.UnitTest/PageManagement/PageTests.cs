using Fix.Data;
using IdentityServer.Models.PageManagement;
using IdentityServer.PageManagement.Services;
using IdentityServer.Security.Services;
using IdentityServer.UnitTest.Facades;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace IdentityServer.UnitTest.ApplicationManagement.PageManagement
{
    public class PageTests : TestBase
    {
        public PageTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Page_Repository_Add()
        {
            var repo = ContainerManager.Resolve<IRepository<Page>>();
            var page = new Page()
            {
                Parent = null,
                ComponentName = null,
                Icon = "test",
                Name = "test",
                Url = "test",
            };
            repo.Add(page);
            SaveChanges();
            var x = repo.Table.Where(en => en.Name == "test");
            Assert.NotNull(x);
            Assert.NotEmpty(x);
        }

        [Fact]
        public void Page_Get_With_Role_Add()
        {
            var pageService = ContainerManager.Resolve<IPageService>();
            var accountService = ContainerManager.Resolve<IAccountService>();
            var roles = accountService.GetRoles();
            var role = roles.First();

            var PageRoles = new List<int>()
                    {
                        role.Id
                    };
            var page = new Page()
            {
                ComponentName = null,
                Icon = "test",
                Name = "test",
                Url = "test",
            };
            pageService.Create(page, null, PageRoles);
            SaveChanges();
            var pages = pageService.Get();
            Assert.NotNull(pages);
            Assert.NotEmpty(pages);
            Assert.NotNull(pages.First().PageRoles);
            Assert.NotEmpty(pages.First().PageRoles);
        }

        [Fact]
        public void Page_Child_And_Parent_Test()
        {
            var pageService = ContainerManager.Resolve<IPageService>();
            var accountService = ContainerManager.Resolve<IAccountService>();
            var roles = accountService.GetRoles();
            var role = roles.First();

            var PageRoles = new List<int>()
                    {
                        role.Id
                    };

            var page1 = new Page()
            {
                ComponentName = null,
                Icon = "test1",
                Name = "test1",
                Url = "test1",
                IsActive = true,
                ShowOnMenu = true,
            };
            pageService.Create(page1, null, PageRoles);
            SaveChanges();
            var page2 = new Page()
            {
                ComponentName = null,
                Icon = "test2",
                Name = "test2",
                Url = "test2",
                IsActive = true,
                ShowOnMenu = true,
            };
            pageService.Create(page2, page1.Id, PageRoles);
            var page3 = new Page()
            {
                ComponentName = null,
                Icon = "test3",
                Name = "test3",
                Url = "test3",
                IsActive = true,
                ShowOnMenu = true,
            };
            pageService.Create(page3, page1.Id, PageRoles);
            SaveChanges();
            var page4 = new Page()
            {
                ComponentName = null,
                Icon = "test4",
                Name = "test4",
                Url = "test4",
                IsActive = true,
                ShowOnMenu = true,
            };
            pageService.Create(page4, page3.Id, PageRoles);
            SaveChanges();
            var page5 = new Page()
            {
                ComponentName = null,
                Icon = "test5",
                Name = "test5",
                Url = "test5",
                IsActive = true,
                ShowOnMenu = true,
            };
            pageService.Create(page5, page4.Id, PageRoles);
            SaveChanges();
            var pages = pageService.Get();
            Assert.NotNull(pages);
            Assert.NotEmpty(pages);
            Assert.True(pages.First().IsActive);
            Assert.NotNull(pages.First().PageRoles);
            Assert.NotNull(pages.First().PageRoles.ToList()[0].Role);
            Assert.NotEmpty(pages.First().PageRoles);
            Assert.NotNull(pages.First(en => en.Name == "test1").Children);
            Assert.NotEmpty(pages.First(en => en.Name == "test1").Children);
            Assert.NotNull(pages.First(en => en.Name == "test2").Parent);

            var userPages = pageService.GetUserPages(new List<int>() { role.Id }).ToList();
            Assert.True(userPages?.Count() == 1);
            Assert.True(userPages[0].Children.ToList()[1].Children.ToList()[0].Name == "test4");
        }


    }

}
