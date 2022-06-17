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
    public class InstructorStudentRelationController : Controller
    {
        private readonly IStudentDomain studentDomain;

        public InstructorStudentRelationController(IStudentDomain studentDomain)
        {
            this.studentDomain = studentDomain ?? throw new ArgumentNullException(nameof(studentDomain));
        }

        #region Student InstructorRelations
        [HttpGet("{instructorId}")]
        public IActionResult Get(long instructorId)//
        {
            var studentInstructorRelations = studentDomain.StudentInstructorRelationService.GetStudentInstructorRelationsForInstructor(instructorId);
            var response = StudentInstructorRelationModeller.ToGetStudentInstructorRelationsResponse(studentInstructorRelations);
            return Ok(response);
        }

        // GET api/values/5
        [HttpGet("{instructorId}/{Id}")]
        public IActionResult GetById(long instructorId, int id)
        {
            var studentInstructorRelation = studentDomain.StudentInstructorRelationService.GetStudentInstructorRelationById(id);
            var response = StudentInstructorRelationModeller.ToGetStudentInstructorRelationResponse(studentInstructorRelation);
            return Ok(response);
        }


        // POST api/values
        [HttpPost("{instructorId}")]
        public IActionResult Post(long instructorId, [FromBody]CreateStudentInstructorRelationRequest request)
        {
            var studentInstructorRelation = request.CreateStudentInstructorRelationRequestToModel();
            studentInstructorRelation.Student = studentDomain.StudentService.GetStudentById(request.Student);
            studentInstructorRelation.Instructor = studentDomain.InstructorService.GetInstructorById(request.Instructor);
            // studentInstructorRelation.Branch = studentDomain.BranchService.GetById(request.Branch);
            studentDomain.StudentInstructorRelationService.CreateStudentInstructorRelation(studentInstructorRelation);
            return Ok(new BaseServiceResponse());
        }

        // PUT api/values/5
        [HttpPut("{instructorId}/{Id}")]
        public IActionResult Put(long instructorId, long id, [FromBody]UpdateStudentInstructorRelationRequest request)
        {
            var studentInstructorRelation = studentDomain.StudentInstructorRelationService.GetStudentInstructorRelationById(id);
            request.UpdateStudentInstructorRelationRequestToModel(ref studentInstructorRelation);
            studentInstructorRelation.Instructor = studentDomain.InstructorService.GetInstructorById(request.Instructor);
            // studentInstructorRelation.Branch = studentDomain.BranchService.GetById(request.Branch);
            studentDomain.StudentInstructorRelationService.UpdateStudentInstructorRelation(studentInstructorRelation);
            return Ok(new BaseServiceResponse());
        }

        // DELETE api/values/5
        [HttpDelete("{instructorId}/{Id}")]
        public IActionResult Delete(long instructorId, int id)
        {
            studentDomain.StudentInstructorRelationService.DeleteStudentInstructorRelation(id);
            return Ok(new BaseServiceResponse());
        }
        #endregion
    }
}
