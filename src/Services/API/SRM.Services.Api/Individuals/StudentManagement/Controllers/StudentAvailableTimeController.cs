using Fix.Data;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SRM.Domain.Individuals.StudentManagement;
using SRM.Domain.Shuttles.OperationManagement.Services;
using SRM.Domain.Times.Services;
using SRM.Services.Api.BaseModel;
using SRM.Services.Api.Individuals.StudentManagement.Models;
using System;

namespace SRM.Services.Api.Individuals.StudentManagement.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    public class StudentAvailableTimeController : Controller
    {
        private readonly IStudentDomain studentDomain;
        private readonly IDateCombinationService dateCombinationService;
        private readonly ITransactionManager transactionManager;
        private readonly IShuttleService shuttleService;

        public StudentAvailableTimeController(IStudentDomain studentDomain,
            IDateCombinationService dateCombinationService,
            ITransactionManager transactionManager,
            IShuttleService shuttleService)
        {
            this.studentDomain = studentDomain ?? throw new ArgumentNullException(nameof(studentDomain));
            this.dateCombinationService = dateCombinationService;
            this.transactionManager = transactionManager;
            this.shuttleService = shuttleService;
        }

        #region Student AvailableTimes
        [HttpGet("{studentId}")]
        public IActionResult Get(long studentId)//
        {
            var studentAvailableTimes = studentDomain.StudentAvailableTimeService.Get(studentId);
            var response = StudentAvailableTimeModeller.ToGetStudentAvailableTimesResponse(studentAvailableTimes);
            return Ok(response);
        }

        // GET api/values/5
        [HttpGet("{studentId}/{Id}")]
        public IActionResult GetById(long studentId, int id)
        {
            var studentAvailableTime = studentDomain.StudentAvailableTimeService.GetById(studentId, id);
            var response = StudentAvailableTimeModeller.ToGetStudentAvailableTimeResponse(studentAvailableTime);
            return Ok(response);
        }

        // POST api/values
        [HttpPost("{studentId}")]
        public IActionResult Post(long studentId, [FromBody]CreateStudentAvailableTimeRequest request)
        {
            var studentAvailableTime = request.CreateStudentAvailableTimeRequestToModel();
            studentAvailableTime.Student = studentDomain.StudentService.GetStudentById(studentId);
            if (!request.IsIntegrated)
                studentAvailableTime.IncludedDate = dateCombinationService.GetDateCombination(request.IncludedDate);
            studentDomain.StudentAvailableTimeService.Create(studentAvailableTime);
            transactionManager.Commit();
            shuttleService.ChangeStudentOperationByAvaibleTime(studentAvailableTime.Id);
            return Ok(new BaseServiceResponse());
        }

        // PUT api/values/5
        [HttpPut("{studentId}/{Id}")]
        public IActionResult Put(long studentId, long id, [FromBody]UpdateStudentAvailableTimeRequest request)
        {
            var studentAvailableTime = studentDomain.StudentAvailableTimeService.GetById(studentId, id);
            request.UpdateStudentAvailableTimeRequestToModel(ref studentAvailableTime);
            studentAvailableTime.Student = studentDomain.StudentService.GetStudentById(studentId);
            if (!request.IsIntegrated)
                studentAvailableTime.IncludedDate = dateCombinationService.GetDateCombination(request.IncludedDate);
            studentDomain.StudentAvailableTimeService.Update(studentAvailableTime);
            return Ok(new BaseServiceResponse());
        }

        // DELETE api/values/5
        [HttpDelete("{studentId}/{Id}")]
        public IActionResult Delete(int studentId, int id)
        {
            studentDomain.StudentAvailableTimeService.Delete(studentId, id);
            return Ok(new BaseServiceResponse());
        }
        #endregion
    }
}
