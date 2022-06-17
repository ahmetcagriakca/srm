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
    public class HospitalController : ControllerBase
    {
        private readonly IHospitalService service;

        public HospitalController(IHospitalService service)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
        }

        // GET: api/Hospital
        [HttpGet]
        public IActionResult Get()
        {
            var entities = service.Get();
            var response = HospitalModeller.ToResponse(entities);
            return Ok(response);
        }

        // GET: api/Hospital/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var entity = service.GetById(id);
            var response = HospitalModeller.ToResponse(entity);
            return Ok(response);
        }

        // GET: api/Hospital/Search
        [HttpGet("Search")]
        public IActionResult Search(SearchHospitalRequest request)
        {
            var student = service.Search(en => (request.Id == null || en.Id == request.Id)
             && (request.Name.IsNullOrEmpty() || en.Name == request.Name)
              && (request.IsActive == null || en.IsActive == request.IsActive));
            var response = HospitalModeller.ToResponse(student);
            return Ok(response);
        }

        // POST: api/Hospital
        [HttpPost]
        public IActionResult Post([FromBody] CreateHospitalRequest request)
        {
            var entity = request.ToModel();
            service.Create(entity);
            return Ok(new BaseServiceResponse());
        }

        // PUT: api/Hospital/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateHospitalRequest request)
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
