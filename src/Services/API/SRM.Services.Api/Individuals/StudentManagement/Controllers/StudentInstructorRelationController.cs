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
    public class StudentInstructorRelationController : Controller
    {
        private readonly IStudentDomain studentDomain;

        public StudentInstructorRelationController(IStudentDomain studentDomain)
        {
            this.studentDomain = studentDomain ?? throw new ArgumentNullException(nameof(studentDomain));
        }

        #region Student InstructorRelations
        [HttpGet("{studentId}")]
        public IActionResult Get(long studentId)//
        {
            var studentInstructorRelations = studentDomain.StudentInstructorRelationService.GetStudentInstructorRelations(studentId);
            var response = StudentInstructorRelationModeller.ToGetStudentInstructorRelationsResponse(studentInstructorRelations);
            return Ok(response);
        }


        // GET api/values/5
        [HttpGet("{studentId}/{Id}")]
        public IActionResult GetById(long studentId, int id)
        {
            var studentInstructorRelation = studentDomain.StudentInstructorRelationService.GetStudentInstructorRelationById(id);
            var response = StudentInstructorRelationModeller.ToGetStudentInstructorRelationResponse(studentInstructorRelation);
            return Ok(response);
        }


        // POST api/values
        [HttpPost("{studentId}")]
        public IActionResult Post(long studentId, [FromBody]CreateStudentInstructorRelationRequest request)
        {
            var studentInstructorRelation = request.CreateStudentInstructorRelationRequestToModel();
            studentInstructorRelation.Student = studentDomain.StudentService.GetStudentById(request.Student);
            studentInstructorRelation.Instructor = studentDomain.InstructorService.GetInstructorById(request.Instructor);
            // studentInstructorRelation.Branch = studentDomain.BranchService.GetById(request.Branch);
            studentDomain.StudentInstructorRelationService.CreateStudentInstructorRelation(studentInstructorRelation);
            return Ok(new BaseServiceResponse());
        }

        // PUT api/values/5
        [HttpPut("{studentId}/{Id}")]
        public IActionResult Put(long studentId, long id, [FromBody]UpdateStudentInstructorRelationRequest request)
        {
            var studentInstructorRelation = studentDomain.StudentInstructorRelationService.GetStudentInstructorRelationById(id);
            request.UpdateStudentInstructorRelationRequestToModel(ref studentInstructorRelation);
            studentInstructorRelation.Instructor = studentDomain.InstructorService.GetInstructorById(request.Instructor);
            // studentInstructorRelation.Branch = studentDomain.BranchService.GetById(request.Branch);
            studentDomain.StudentInstructorRelationService.UpdateStudentInstructorRelation(studentInstructorRelation);
            return Ok(new BaseServiceResponse());
        }

        // DELETE api/values/5
        [HttpDelete("{studentId}/{Id}")]
        public IActionResult Delete(long studentId, int id)
        {
            studentDomain.StudentInstructorRelationService.DeleteStudentInstructorRelation(id);
            return Ok(new BaseServiceResponse());
        }
        #endregion
    }
}
