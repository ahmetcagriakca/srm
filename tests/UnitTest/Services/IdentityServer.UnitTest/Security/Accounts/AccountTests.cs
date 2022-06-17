using Fix.Security.Cryptography;
using Fix.Mvc;
using IdentityServer.Security;
using IdentityServer.Security.Controllers;
using IdentityServer.Security.Exceptions;
using IdentityServer.Security.Models;
using IdentityServer.UnitTest.Facades;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace IdentityServer.UnitTest.Security.Accounts
{
    public class AccountTests : TestBase
    {
        private readonly ISecurityDomain securityDomain;
        private readonly ICryptoService cryptoService;
        private readonly AccountController accountController;
        public AccountTests(ITestOutputHelper output):base (output)
        {
            securityDomain = ContainerManager.Resolve<ISecurityDomain>();
            cryptoService = ContainerManager.Resolve<ICryptoService>();
            accountController = new AccountController(securityDomain);
        }

        [Theory]
        [InlineData("admin", "123456", true)]

        public void Change_UserPassword(string userName, string password, bool result)
        {
            var entity = securityDomain.Account.GetUser(userName);
            var actionResult = accountController.ChangePassword(new AccountChangePasswordRequest()
            {
                UserId = entity.Id,
                OldPassword = userName,
                NewPassword = password
            });
            Assert.Equal(result, actionResult.Value.State == ResultState.Successfully);
            var user = securityDomain.Account.GetUser(entity.Id);
            Assert.Equal(result, user.Password == cryptoService.Encrypt(password, HashTypes.MD5).Value);
        }

        [Theory]
        [InlineData(1, true)]
        public void GetUsers_WithPaging(int pageIndex, bool result)
        {
            var serviceResult = accountController.Index(pageIndex).Value;
            Assert.Equal(result, serviceResult.State == ResultState.Successfully);
            Assert.Equal(result, serviceResult.Result.Any());
        }

        public static IEnumerable<object[]> UserExistingData => new List<object[]>
        {
            new object[] { "admin","admin","admin","123456",1, DateTime.Now},
        };

        [Theory, MemberData(nameof(UserExistingData))]
        public void CreateUser_CheckUserAlreadyExist(string userName, string name, string surname, string password, int roleId, DateTime createDate)
        {
            var account = new CreateAccountRequest
            {
                UserName = userName,
                Name = name,
                Surname = surname,
                Password = password,
                RoleId = roleId,
                CreatedOn = createDate

            };
            Assert.Throws<UserAlreadyExistsException>(() => accountController.CreateAccount(account));
        }

        public static IEnumerable<object[]> CreateUserSuccessfullyData => new List<object[]>
        {
            new object[] { "admin1","admin","admin","123456",1, DateTime.Now, true},
        };

        [Theory, MemberData(nameof(CreateUserSuccessfullyData))]
        //[InlineData("admin1", "admin", "admin", "123456", 1, DateTime.Now, true)]
        public void CreateUser_Successfully(string userName, string name, string surname, string password, int roleId, DateTime createDate, bool result)
        {
            var account = new CreateAccountRequest
            {
                UserName = userName,
                Name = name,
                Surname = surname,
                Password = password,
                RoleId = roleId,
                CreatedOn = createDate
            };
            var actionResult = accountController.CreateAccount(account);
            Assert.Equal(result, actionResult.Value.State == ResultState.Successfully);
            SaveChanges();
            var entity = securityDomain.Account.GetUser(userName);
            Assert.NotNull(entity);
        }

        [Theory]
        [InlineData(1, true)]
        public void GetUser_CheckValue(int pageIndex, bool result)
        {
            var entity = securityDomain.Account.GetUser("admin");
            output.WriteLine(entity.Id.ToString());
            var actionResult = accountController.Show(pageIndex);
            Assert.Equal(result, actionResult.Value.State == ResultState.Successfully);
            Assert.Equal(result, actionResult.Value.Result.Name=="admin");
        }
    }
}
