using Fix.Mvc.Filters;
using Fix.Mvc;
using IdentityServer.Security.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Serilog;
using Serilog.Events;

namespace IdentityServer.Security.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly ISecurityDomain securityDomain;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="securityDomain"></param>
        /// <param name="logger"></param>
        public AccountController(
            ISecurityDomain securityDomain
            )
        {
            this.securityDomain = securityDomain ?? throw new ArgumentNullException(nameof(securityDomain));
            Log.Information("test");
        }

        /// <summary>
        /// User lists with paging
        /// </summary>
        /// <remarks>Good Comments!</remarks>
        /// <param name="pageIndex">Index of the page</param>
        /// <example>1</example>
        /// <returns></returns>
        [HttpGet("page/{pageIndex}")]
        [FixAuthorization(Permission = AccountPermissions.VIEW_USERS)]
        public ActionResult<ServiceResult<IEnumerable<GetUserResponse>>> Index(int pageIndex)
        {
            var users = securityDomain.Account.GetUsers(pageIndex);
            var response = users.ToAccountIndexResponse();
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        [FixAuthorization(Permission = AccountPermissions.REGISTER_USER)]
        public ActionResult<ServiceResult> CreateAccount([FromBody]CreateAccountRequest request)
        {
            var user = request.ToModel();
            securityDomain.Account.Register(user);
            return Ok(new ServiceResult());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [FixAuthorization(Permission = AccountPermissions.MANAGE_USER)]
        public ActionResult<ServiceResult> Update(int id, [FromBody] AccountUpdateRequest request)
        {
            var user = securityDomain.Account.GetUserWithRelations(id);
            request.ToModel(user);
            securityDomain.Account.UpdateUser(user);
            return Ok(new ServiceResult());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("activate/{userId}")]
        [FixAuthorization(Permission = AccountPermissions.ACTIVATE_USER)]
        public ActionResult<ServiceResult> Activate(int userId)
        {
            securityDomain.Account.Activate(userId);
            return Ok(new ServiceResult());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("show/{id}")]
        [FixAuthorization(Permission = AccountPermissions.VIEW_USER)]
        public ActionResult<ServiceResult<GetUserResponse>> Show(int id)
        {
            var user = securityDomain.Account.GetUserWithRelations(id);
            var response = user.ToResponse();
            return Ok(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("changePassword")]
        [FixAuthorization(Permission = AccountPermissions.CHANGE_PASSWORD)]
        public ActionResult<ServiceResult> ChangePassword([FromBody]AccountChangePasswordRequest request)
        {
            securityDomain.Account.ChangePassword(request.UserId, request.OldPassword, request.NewPassword);
            return Ok(new ServiceResult());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("AddUserRole/{userId}")]
        [FixAuthorization(Permission = AccountPermissions.REGISTER_USER)]
        public ActionResult<ServiceResult> AddUserRole(long userId, [FromBody]AddUserRoleRequest request)
        {
            securityDomain.Account.AddUserRole(userId, request.RoleId);
            return Ok(new ServiceResult());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("CreateRole")]
        [FixAuthorization(Permission = AccountPermissions.REGISTER_USER)]
        public ActionResult<ServiceResult> CreateRole([FromBody]RoleStoreRequest request)
        {
            var role = request.ToModel();
            securityDomain.Account.AddRole(role);
            return Ok(new ServiceResult());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("UpdateRole/{id}")]
        [FixAuthorization(Permission = AccountPermissions.REGISTER_USER)]
        public ActionResult<ServiceResult> UpdateRole(int id, [FromBody]RoleUpdateRequest request)
        {
            var role = securityDomain.Account.GetRoleWithRelations(id);
            request.BindTo(role);
            securityDomain.Account.SaveRole(role);
            return Ok(new ServiceResult());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetRoles")]
        [FixAuthorization(Permission = AccountPermissions.VIEW_USER)]
        public ActionResult<ServiceResult> GetRoles()
        {
            var roles = securityDomain.Account.GetRoles();
            var response = roles.ToResponse();
            return Ok(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetDrivers")]
        public ActionResult<ServiceResult> GetDrivers()
        {
            var entity = securityDomain.Account.GetUsersByRoles("Driver");
            var response = entity.ToAccountResponse();
            return Ok(response);
        }
    }
}