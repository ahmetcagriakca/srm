using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SRM.Domain.Individuals.InstructorManagement;
using SRM.Services.Api.BaseModel;
using SRM.Services.Api.Individuals.InstructorManagement.Models;

namespace SRM.Services.Api.Individuals.InstructorManagement.Controllers
{

    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    public class InstructorController : Controller
    {
        private readonly IInstructorDomain instructorDomain;

        public InstructorController(IInstructorDomain instructorDomain)
        {
            this.instructorDomain = instructorDomain;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var instructors = instructorDomain.InstructureService.GetInstructors();
            var response = InstructorModeller.ToGetInstructorsResponse(instructors);
            return Ok(response);
        }

        // GET: api/values
        [HttpGet("Search")]
        public IActionResult Search(SearchInstructorRequest request)
        {
            var instructor = instructorDomain.InstructureService.Search(request.Id, request.Name, request.Surname, request.IdentityNumber, request.Branch, request.IsActive);
            var response = InstructorModeller.ToSearchInstructorsResponse(instructor);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Post([FromBody]CreateInstructorRequest request)
        {
            var instructor = request.CreateRequestToModel();

            instructorDomain.InstructureService.CreateInstructor(instructor);
            return Ok(new BaseServiceResponse());
        }

        [HttpGet("GetByIdentityNumber/{IdentityNumber}")]
        public IActionResult GetInstructorByIdentityNumber(string identityNumber)
        {
            var instructor = instructorDomain.InstructureService.GetInstructorByIdentityNumber(identityNumber);
            var response = InstructorModeller.ToGetInstructorResponse(instructor);
            return Ok(response);
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(long id)
        {
            var instructor = instructorDomain.InstructureService.GetInstructorById(id);
            var response = InstructorModeller.ToGetInstructorResponse(instructor);
            return Ok(response);
        }
        [HttpPut("{Id}")]
        public IActionResult Put(long id, [FromBody]UpdateInstructorRequest request)
        {
            var instructor = instructorDomain.InstructureService.GetInstructorById(id);
            request.UpdateRequestToModel(ref instructor);

            instructorDomain.InstructureService.UpdateInstructor(instructor);
            //instructorDomain.InstructureService.UpdateInstructorBranches(instructor, request.Branches);
            return Ok(new BaseServiceResponse());
        }

        [HttpDelete]
        public IActionResult Delete(long id)
        {
            instructorDomain.InstructureService.DeleteInstructor(id);
            return Ok(new BaseServiceResponse());
        }


        //TODO: Öğretmen Branch Tanımını mevcut olusturmadan kaldırdım.ayrı metod olarak branch işlemlerini ekleyelim.
        //TODO: Öğretmen içerisinde adres bilgisi kaldırdım.
    }
}
