using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SRM.Domain;
using SRM.Domain.Parameters.Services;
using SRM.Services.Api.BaseModel;
using SRM.Services.Api.Parameters.Models;
using System;

namespace SRM.Services.Api.Parameters.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    public class ApplicationParameterController : ControllerBase
    {
        private readonly IApplicationParameterService service;

        public ApplicationParameterController(IApplicationParameterService service)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
        }

        // GET: api/ApplicationParameter
        [HttpGet]
        public IActionResult Get()
        {
            var entities = service.Get();
            var response = ApplicationParameterModeller.ToResponse(entities);
            return Ok(response);
        }

        // GET: api/ApplicationParameter
        [HttpGet("GetByName/{name}")]
        public IActionResult GetByName(string name)
        {
            var entities = service.GetListByName(name);
            var response = ApplicationParameterModeller.ToResponse(entities);
            return Ok(response);
        }

        // GET: api/ApplicationParameter/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var entity = service.GetById(id);
            var response = ApplicationParameterModeller.ToResponse(entity);
            return Ok(response);
        }

        // GET: api/ApplicationParameter/Search
        [HttpGet("Search")]
        public IActionResult Search(SearchApplicationParameterRequest request)
        {
            var student = service.Search(en => (request.Id == null || en.Id == request.Id)
             && (request.Name.IsNullOrEmpty() || en.Name.Contains(request.Name))
              && (request.IsActive == null || en.IsActive == request.IsActive)
              );
            var response = ApplicationParameterModeller.ToResponse(student);
            return Ok(response);
        }

        // POST: api/ApplicationParameter
        [HttpPost]
        public IActionResult Post([FromBody] CreateApplicationParameterRequest request)
        {
            var entity = request.ToModel();
            service.Create(entity);
            return Ok(new BaseServiceResponse());
        }

        // PUT: api/ApplicationParameter/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateApplicationParameterRequest request)
        {
            var entity = service.GetById(id);
            entity = request.ToModel(entity);
            service.Update(entity);
            return Ok(new BaseServiceResponse());
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            service.Delete(id);
            return Ok(new BaseServiceResponse());
        }
    }
}
