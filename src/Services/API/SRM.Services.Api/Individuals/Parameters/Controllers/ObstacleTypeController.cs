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
    public class ObstacleTypeController : ControllerBase
    {
        private readonly IObstacleTypeService service;

        public ObstacleTypeController(IObstacleTypeService service)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
        }

        // GET: api/ObstacleType
        [HttpGet]
        public IActionResult Get()
        {
            var entities = service.Get();
            var response = ObstacleTypeModeller.ToResponse(entities);
            return Ok(response);
        }

        // GET: api/ObstacleType/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var entity = service.GetById(id);
            var response = ObstacleTypeModeller.ToResponse(entity);
            return Ok(response);
        }

        // GET: api/ObstacleType/Search
        [HttpGet("Search")]
        public IActionResult Search(SearchObstacleTypeRequest request)
        {
            var student = service.Search(en => (request.Id == null || en.Id == request.Id)
             && (request.Name.IsNullOrEmpty() || en.Name == request.Name)
              && (request.Description.IsNullOrEmpty() || en.Description == request.Description)
              && (request.IsActive == null || en.IsActive == request.IsActive));
            var response = ObstacleTypeModeller.ToResponse(student);
            return Ok(response);
        }

        // POST: api/ObstacleType
        [HttpPost]
        public IActionResult Post([FromBody] CreateObstacleTypeRequest request)
        {
            var entity = request.ToModel();
            service.Create(entity);
            return Ok(new BaseServiceResponse());
        }

        // PUT: api/ObstacleType/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateObstacleTypeRequest request)
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
