using IdentityServer.Models;
using IdentityServer.Security;
using IdentityServer.Security.Exceptions;
using IdentityServer.Security.Models;
using IdentityServer.UnitTest.Facades;
using Xunit;
using Xunit.Abstractions;

namespace IdentityServer.UnitTest.Security.Accounts
{
    public class AuthenticationTest : TestBase
    {
        private readonly ISecurityDomain securityDomain;
        public AuthenticationTest(ITestOutputHelper output) : base(output)
        {
            securityDomain = ContainerManager.Resolve<ISecurityDomain>();
        }

        [Theory]
        [InlineData("admin", "admin", true)]
        public void Try_Login_Successfully(string userName, string password, bool result)
        {
            var userHasLogin = securityDomain.Account.TryGetUserContext(userName, password, out _, out _);
            Assert.True(userHasLogin == result);
        }

        [Theory]
        [InlineData("admin", "password")]
        public void Try_Login_WithException(string userName, string password)
        {
            Assert.Throws<PasswordMismatchException>(() => securityDomain.Account.TryGetUserContext(userName, password, out ClientContext clientContext,
                out User user));
        }
    }
}
