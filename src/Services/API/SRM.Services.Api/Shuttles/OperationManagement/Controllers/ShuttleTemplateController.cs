using Microsoft.AspNetCore.Mvc;
using SRM.Domain.Shuttles.OperationManagement.Services;
using SRM.Services.Api.BaseModel;
using SRM.Services.Api.Shuttles.OperationManagement.Models;

namespace SRM.Services.Api.Shuttles.OperationManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShuttleTemplateController : ControllerBase
    {
        private readonly IShuttleTemplateService shuttleTemplateService;

        public ShuttleTemplateController(IShuttleTemplateService shuttleTemplateService)
        {
            this.shuttleTemplateService = shuttleTemplateService;
        }

        //Günlük servis taslaklarını getir
        [HttpGet("GetShuttleTemplateByDayOfWeek/{dayOfWeek}")]
        public IActionResult GetShuttleTemplateByDayOfWeek(int? dayOfWeek)
        {
            var shuttleTemplates = shuttleTemplateService.GetShuttleTemplateByDayOfWeek(dayOfWeek == 0 ? null : dayOfWeek);
            var response = shuttleTemplates.GetShuttleTemplateListResponse();
            return Ok(response);
        }

        //Servis taslagının öğrenci taslaklarını getir
        [HttpGet("GetStudentTemplateByShuttleTemplateId/{shuttleTemplateId}")]
        public IActionResult GetStudentTemplateByShuttleTemplateId(int shuttleTemplateId)
        {
            var studentTemplates = shuttleTemplateService.GetStudentTemplateByShuttleTemplateId(shuttleTemplateId);
            var response = studentTemplates.GetStudentTemplateListResponse();
            return Ok(response);
        }

        //Servis Taslagını Ekle
        [HttpPost]
        public IActionResult CreateShuttleTemplate([FromBody]CreateShuttleTemplateRequest request)
        {
            var shuttleTemplate = request.CreateShuttleTemplateToModel();
            shuttleTemplateService.CreateShuttleTemplate(shuttleTemplate, request.LocationRegionId, request.StudentServiceId);
            return Ok(new BaseServiceResponse());
        }



        // PUT: api/LocationRegion/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateShuttleTemplateRequest request)
        {
            var entity = request.ToModel();
            shuttleTemplateService.UpdateShuttleTemplate(entity, request.LocationRegionId, request.StudentServiceId);
            return Ok(new BaseServiceResponse());
        }

        //Servis Taslagını sil
        [HttpDelete("{id}")]
        public IActionResult DeleteShuttleTemplate(int id)
        {
            shuttleTemplateService.DeleteShuttleTemplate(id);
            return Ok(new BaseServiceResponse());
        }

        //Öğrenci servis taslagını ekle
        [HttpPost("CreateStudentTemplate")]
        public IActionResult CreateStudentTemplate(CreateStudentTemplateRequest request)
        {
            var studentTemplate = request.CreateStudentTemplateToModel();
            shuttleTemplateService.CreateStudentTemplate(request.ShuttleTemplateId, request.StudentId, studentTemplate);
            return Ok(new BaseServiceResponse());
        }

        //Öğrenci servis taslagını ekle
        [HttpPut("UpdateStudentTemplate")]
        public IActionResult UpdateStudentTemplate(UpdateStudentTemplateRequest request)
        {
            // var studentTemplate = request.UpdateStudentTemplateToModel();
            shuttleTemplateService.UpdateStudentTemplate(request.StudentTemplateId, request.Order, request.LessonCount);
            return Ok(new BaseServiceResponse());
        }

        //Öğrenci Taslagını sil
        [HttpDelete("DeleteStudentTemplate/{shuttleStudentTemplateId}")]
        public IActionResult DeleteStudentTemplate(int shuttleStudentTemplateId)
        {
            shuttleTemplateService.DeleteStudentTemplate(shuttleStudentTemplateId);
            return Ok(new BaseServiceResponse());
        }

        //Bütün servis taslaklarını getir
        [HttpGet("GetAllShuttleTemplate")]
        public IActionResult GetAllShuttleTemplate()
        {
            var shuttleTemplates = shuttleTemplateService.GetAllShuttleTemplate();
            var response = shuttleTemplates.GetShuttleTemplateHeaderListResponse();
            return Ok(response);
        }
        //TODO: öğrenci taslagı id ye getirme istenirse sadece controller metodu yazılacak
        // ShuttleStudentTemplate GetStudentTemplateById(int studentTemplateId);
    }
}