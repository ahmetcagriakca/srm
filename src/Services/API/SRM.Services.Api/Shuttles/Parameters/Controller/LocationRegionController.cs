using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SRM.Domain;
using SRM.Domain.Shuttles.Parameters.Services;
using SRM.Services.Api.BaseModel;
using SRM.Services.Api.Shuttles.Parameters.Models;
using System;
using System.Linq;

namespace SRM.Services.Api.Shuttles.Parameters.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    public class LocationRegionController : ControllerBase
    {
        private readonly ILocationRegionService service;

        public LocationRegionController(ILocationRegionService service)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
        }

        // GET: api/LocationRegion
        [HttpGet]
        public IActionResult Get()
        {
            var entities = service.Get();
            var response = LocationRegionModeller.ToResponse(entities);
            return Ok(response);
        }

        [HttpGet("GetActiveRegions")]
        public IActionResult GetActiveRegions()
        {
            var entities = service.GetActiveRegions();
            var response = LocationRegionModeller.ToResponse(entities);
            return Ok(response);
        }

        // GET: api/LocationRegion/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var entity = service.GetById(id);
            var response = LocationRegionModeller.ToResponse(entity);
            return Ok(response);
        }

        // GET: api/LocationRegion/Search
        [HttpGet("Search")]
        public IActionResult Search(SearchLocationRegionRequest request)
        {
            var student = service.Search(en => (request.Id == null || en.Id == request.Id)
             && (request.Name.IsNullOrEmpty() || en.Name.Contains(request.Name))
             && (request.Code == null || en.Code.ToString().Contains(request.Code.ToString()))
              && (request.IsActive == null || en.IsActive == request.IsActive)
              && (request.SubRegion == null || en.RegionRelations.Any(entity => entity.SubRegion.Id == request.SubRegion)));
            var response = LocationRegionModeller.ToResponse(student);
            return Ok(response);
        }

        // POST: api/LocationRegion
        [HttpPost]
        public IActionResult Post([FromBody] CreateLocationRegionRequest request)
        {
            var entity = request.ToModel();
            service.Create(entity, request.SubRegionIds);
            return Ok(new BaseServiceResponse());
        }

        // PUT: api/LocationRegion/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateLocationRegionRequest request)
        {
            var entity = service.GetById(id);
            entity = request.ToModel(entity);
            service.Update(entity, request.SubRegionIds);
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
