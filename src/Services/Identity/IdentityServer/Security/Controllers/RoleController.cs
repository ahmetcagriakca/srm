using Fix.Mvc;
using IdentityServer.Security.Models;
using IdentityServer.Security.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Srm.Services.Api.Security.Accounts.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    public class RoleController : Controller
    {
        private readonly IRoleService service;

        public RoleController(IRoleService service)
        {
            this.service = service;
        }

        // GET: api/Role
        [HttpGet]
        public IActionResult Get()
        {
            var result = service.Get();
            var response = result.ToResponse();
            return Ok(response);
        }

        // GET: api/values
        [HttpGet("Search")]
        public IActionResult Search(SearchRoleRequest request)
        {
            var entities = service.Search(request.Id, request.Name, request.Description, request.IsActive);
            var response = entities.ToResponse();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = service.GetById(id);
            return Ok(result.ToResponse());
        }

        // POST: api/Role
        [HttpPost]
        public IActionResult Post([FromBody]CreateRoleRequest request)
        {
            var entity = request.CreateRequestToModel();
            service.Create(entity);
            return Ok(new ServiceResult());
        }

        // PUT: api/Role/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]UpdateRoleRequest request)
        {
            var entity = service.GetById(id);
            request.UpdateRequestToModel(ref entity);
            service.Update(entity);
            return Ok(new ServiceResult());
        }

        // DELETE: api/Role/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            service.Delete(id);
            return Ok(new ServiceResult());
        }
    }
}
