using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SRM.Domain.Individuals.StudentManagement;
using SRM.Services.Api.BaseModel;
using SRM.Services.Api.Individuals.StudentManagement.Models;
using System;

namespace SRM.Services.Api.Individuals.StudentManagement.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    public class StudentAddressController : Controller
    {
        private readonly IStudentDomain studentDomain;

        public StudentAddressController(IStudentDomain studentDomain)
        {
            this.studentDomain = studentDomain ?? throw new ArgumentNullException(nameof(studentDomain));
        }

        #region Student Reports
        [HttpGet("{studentId}")]
        public IActionResult Get(long studentId)//
        {
            var StudentAddresss = studentDomain.StudentAddressService.Get(studentId);
            var response = StudentAddressModeller.ToGetStudentAddresssResponse(StudentAddresss);
            return Ok(response);
        }

        // GET api/values/5
        [HttpGet("{studentId}/{Id}")]
        public IActionResult GetById(long studentId, int id)
        {
            var StudentAddress = studentDomain.StudentAddressService.GetById(studentId, id);
            var response = StudentAddressModeller.ToGetStudentAddressResponse(StudentAddress);
            return Ok(response);
        }

        // POST api/values
        [HttpPost("{studentId}")]
        public IActionResult Post(long studentId, [FromBody]CreateStudentAddressRequest request)
        {
            var StudentAddress = request.CreateStudentAddressRequestToModel();
            studentDomain.StudentAddressService.Create(studentId, StudentAddress, request.LocationRegion);
            return Ok(new BaseServiceResponse());
        }

        // PUT api/values/5
        [HttpPut("{studentId}/{Id}")]
        public IActionResult Put(long studentId, long id, [FromBody]UpdateStudentAddressRequest request)
        {
            var StudentAddress = studentDomain.StudentAddressService.GetById(studentId, id);
            request.UpdateStudentAddressRequestToModel(ref StudentAddress);
            studentDomain.StudentAddressService.Update(studentId, StudentAddress, request.LocationRegion);
            return Ok(new BaseServiceResponse());
        }

        // DELETE api/values/5
        [HttpDelete("{studentId}/{Id}")]
        public IActionResult Delete(int studentId, int id)
        {
            studentDomain.StudentAddressService.Delete(studentId, id);
            return Ok(new BaseServiceResponse());
        }
        #endregion
    }
}
