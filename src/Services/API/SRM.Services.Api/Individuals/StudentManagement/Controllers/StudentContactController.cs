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
    public class StudentContactController : Controller
    {
        private readonly IStudentDomain studentDomain;

        public StudentContactController(IStudentDomain studentDomain)
        {
            this.studentDomain = studentDomain ?? throw new ArgumentNullException(nameof(studentDomain));
        }


        [HttpGet("{studentId}")]
        public IActionResult GetContacts(long studentId)//
        {
            var StudentContacts = studentDomain.StudentContactService.Get(studentId);
            var response = StudentContactModeller.ToGetStudentContactsResponse(StudentContacts);
            return Ok(response);
        }

        [HttpGet("{studentId}/{Id}")]
        public IActionResult GetById(long studentId, int id)
        {
            var StudentContact = studentDomain.StudentContactService.GetById(studentId, id);
            var response = StudentContactModeller.ToGetStudentContactResponse(StudentContact);
            return Ok(response);
        }

        [HttpPost("{studentId}")]
        public IActionResult Post(long studentId, [FromBody]CreateStudentContactRequest request)
        {
            var studentContact = request.CreateStudentContactRequestToModel();
            studentDomain.StudentContactService.Create(studentId, studentContact);
            return Ok(new BaseServiceResponse());
        }

        [HttpPut("{studentId}/{Id}")]
        public IActionResult Put(long studentId, long id, [FromBody]UpdateStudentContactRequest request)
        {
            var studentContact = studentDomain.StudentContactService.GetById(studentId, id);
            request.UodateStudentContactRequestToModel(ref studentContact);
            studentDomain.StudentContactService.Update(studentId, studentContact);
            return Ok(new BaseServiceResponse());
        }

        [HttpDelete("{studentId}/{Id}")]
        public IActionResult Delete(int studentId, int id)
        {
            studentDomain.StudentContactService.Delete(studentId, id);
            return Ok(new BaseServiceResponse());
        }

    }
}
