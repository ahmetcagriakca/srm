using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SRM.Domain;
using SRM.Domain.Individuals.Parameters.Services;
using SRM.Services.Api.BaseModel;
using SRM.Services.Api.Individuals.Parameters.Models;
using System;

namespace SRM.Services.Api.Individuals.Parameters.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    public class BranchController : ControllerBase
    {
        private readonly IBranchService service;

        public BranchController(IBranchService service)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
        }

        // GET: api/Branch
        [HttpGet]
        public IActionResult Get()
        {
            var entities = service.Get();
            var response = BranchModeller.ToResponse(entities);
            return Ok(response);
        }

        // GET: api/Branch/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var entity = service.GetById(id);
            var response = BranchModeller.ToResponse(entity);
            return Ok(response);
        }

        // GET: api/Branch/Search
        [HttpGet("Search")]
        public IActionResult Search(SearchBranchRequest request)
        {
            var student = service.Search(en => (request.Id == null || en.Id == request.Id)
             && (request.Name.IsNullOrEmpty() || en.Name == request.Name)
              && (request.Description.IsNullOrEmpty() || en.Description == request.Description)
              && (request.IsActive == null || en.IsActive == request.IsActive));
            var response = BranchModeller.ToResponse(student);
            return Ok(response);
        }

        // POST: api/Branch
        [HttpPost]
        public IActionResult Post([FromBody] CreateBranchRequest request)
        {
            var entity = request.ToModel();
            service.Create(entity);
            return Ok(new BaseServiceResponse());
        }

        // PUT: api/Branch/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateBranchRequest request)
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
