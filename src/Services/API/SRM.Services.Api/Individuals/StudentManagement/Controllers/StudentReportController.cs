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
    public class StudentReportController : Controller
    {
        private readonly IStudentDomain studentDomain;

        public StudentReportController(IStudentDomain studentDomain)
        {
            this.studentDomain = studentDomain ?? throw new ArgumentNullException(nameof(studentDomain));
        }

        #region Student Reports
        [HttpGet("{studentId}")]
        public IActionResult Get(long studentId)//
        {
            var studentReports = studentDomain.StudentReportService.GetStudentReports(studentId);
            var response = StudentReportModeller.ToGetStudentReportsResponse(studentReports);
            return Ok(response);
        }

        // GET api/values/5
        [HttpGet("{studentId}/{Id}")]
        public IActionResult GetById(long studentId, int id)
        {
            var studentReport = studentDomain.StudentReportService.GetStudentReportById(studentId, id);
            var response = StudentReportModeller.ToGetStudentReportResponse(studentReport);
            return Ok(response);
        }

        // POST api/values
        [HttpPost("{studentId}")]
        public IActionResult Post(long studentId, [FromBody]CreateStudentReportRequest request)
        {
            var student = studentDomain.StudentService.GetStudentById(studentId);
            var studentReport = request.CreateStudentReportRequestToModel();
            studentReport.Student = student;
            studentReport.GivenHospital = studentDomain.HospitalService.GetById(request.GivenHospital);
            studentDomain.StudentReportService.CreateStudentReport(studentReport);
            return Ok(new BaseServiceResponse());
        }

        // PUT api/values/5
        [HttpPut("{studentId}/{Id}")]
        public IActionResult Put(long studentId, long id, [FromBody]UpdateStudentReportRequest request)
        {
            var studentReport = studentDomain.StudentReportService.GetStudentReportById(studentId, id);
            request.UpdateStudentReportRequestToModel(ref studentReport);
            studentReport.GivenHospital = studentDomain.HospitalService.GetById(request.GivenHospital);

            studentDomain.StudentReportService.UpdateStudentReport(studentReport);
            return Ok(new BaseServiceResponse());
        }

        // DELETE api/values/5
        [HttpDelete("{studentId}/{Id}")]
        public IActionResult Delete(int studentId, int id)
        {
            studentDomain.StudentReportService.DeleteStudentReport(studentId, id);
            return Ok(new BaseServiceResponse());
        }
        #endregion
    }
}
