using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SRM.Domain;
using SRM.Domain.Individuals.Parameters.Services;
using SRM.Services.Api.BaseModel;
using SRM.Services.Api.Shuttles.Parameters.Models;
using System;

namespace SRM.Services.Api.Shuttles.Parameters.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    public class StudentServiceController : ControllerBase
    {
        private readonly IStudentServiceService service;

        public StudentServiceController(
            IStudentServiceService service
            )
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
        }

        // GET: api/StudentService
        [HttpGet]
        public IActionResult Get()
        {
            var entities = service.Get();
            var response = StudentServiceModeller.ToResponse(entities);
            return Ok(response);
        }

        // GET: api/StudentService/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var entity = service.GetById(id);
            var response = StudentServiceModeller.ToResponse(entity);
            return Ok(response);
        }

        // GET: api/StudentService/Search
        [HttpGet("Search")]
        public IActionResult Search(SearchStudentServiceRequest request)
        {
            var student = service.Search(en => (request.Id == null || en.Id == request.Id)
              && (request.Plate.IsNullOrEmpty() || en.Plate.Contains(request.Plate))
              && (request.MaxCapacity == null || en.MaxCapacity == request.MaxCapacity)
              && (request.Driver == null || en.DriverId == request.Driver)
              && (request.IsActive == null || en.IsActive == request.IsActive));
            var response = StudentServiceModeller.ToResponse(student);
            return Ok(response);
        }

        // POST: api/StudentService
        [HttpPost]
        public IActionResult Post([FromBody] CreateStudentServiceRequest request)
        {
            var entity = request.ToModel();
            service.Create(entity, request.Driver);
            return Ok(new BaseServiceResponse());
        }

        // PUT: api/StudentService/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateStudentServiceRequest request)
        {
            var entity = service.GetById(id);
            entity = request.ToModel(entity);
            service.Update(entity, request.Driver);
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
