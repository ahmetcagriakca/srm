using Fix.Mvc;
using IdentityServer.PageManagement.Models;
using IdentityServer.PageManagement.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.PageManagement.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    public class PageController : Controller
    {
        private readonly IPageService service;

        public PageController(IPageService service)
        {
            this.service = service;
        }

        // GET: api/Page
        [HttpGet]
        public IActionResult Get()
        {
            var result = service.Get();
            var response = result.ToResponse();
            return Ok(response);
        }

        // GET: api/Page
        [HttpGet("GetParents")]
        public IActionResult GetParents()
        {
            var result = service.GetParents();
            var response = result.ToResponse();
            return Ok(response);
        }

        // GET: api/values
        [HttpGet("Search")]
        public IActionResult Search(SearchPageRequest request)
        {
            var entities = service.Search(request.Id, request.Url, request.Name, request.ComponentName, request.Order, request.Icon, request.ShowOnMenu
                , request.Parent, request.PageRoles, request.IsActive);
            var response = entities.ToResponse();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = service.GetById(id);
            return Ok(result.ToResponse());
        }

        // POST: api/Page
        [HttpPost]
        public IActionResult Post([FromBody]CreatePageRequest request)
        {
            var entity = request.CreateRequestToModel();
            service.Create(entity, request.ParentId, request.PageRoleIds);
            return Ok(new ServiceResult());
        }

        // PUT: api/Page/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]UpdatePageRequest request)
        {
            var entity = service.GetById(id);
            request.UpdateRequestToModel(ref entity);
            service.Update(entity, request.ParentId, request.PageRoleIds);
            return Ok(new ServiceResult());
        }

        // DELETE: api/Page/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            service.Delete(id);
            return Ok(new ServiceResult());
        }
    }
}
