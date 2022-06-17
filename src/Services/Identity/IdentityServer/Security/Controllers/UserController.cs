using Fix.Mvc;
using IdentityServer.Models;
using IdentityServer.Security.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using IdentityServer.Security.Models;

namespace SRM.Services.Api.Security.Accounts.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService service;

        public UserController(IUserService service)
        {
            this.service = service;
        }

        // GET: api/User
        [HttpGet]
        public IActionResult Get()
        {
            var entities = service.Get();
            return Json(entities.ToAccountResponse());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var entity = service.GetById(id);
            return Json(entity.ToResponse());
        }

        // POST: api/User
        [HttpPost]
        public IActionResult Post([FromBody]User value)
        {
            service.Create(value);
            return Json(new ServiceResult());
        }

        // PUT: api/User/5
        [HttpPut()]
        public IActionResult Put([FromBody]User value)
        {
            service.Update(value);
            return Json(new ServiceResult());
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            service.Delete(id);
            return Json(new ServiceResult());
        }

    }
}
