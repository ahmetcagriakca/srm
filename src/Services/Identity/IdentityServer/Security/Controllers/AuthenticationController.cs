using Fix.Security;
using IdentityServer.Security;
using IdentityServer.Security.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;

namespace IdentityServer.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly ISecurityDomain securityDomain;
        private readonly IAuthenticationProvider authenticationProvider;

        public AuthenticationController(
            ISecurityDomain securityDomain,
            IAuthenticationProvider authenticationProvider
            )
        {
            this.securityDomain = securityDomain ?? throw new ArgumentNullException(nameof(securityDomain));
            this.authenticationProvider = authenticationProvider ?? throw new ArgumentNullException(nameof(authenticationProvider));
        }

        [HttpGet("GetUserData")]
        public IActionResult GetUserData()
        {
            var userId = authenticationProvider.GetUserId();
            var user = securityDomain.Account.GetUser(userId);
            var response = user.ToResponse();
            return Ok(response);
        }
    }
}