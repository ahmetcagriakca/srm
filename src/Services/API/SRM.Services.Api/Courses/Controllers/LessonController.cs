using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SRM.Domain.Courses.Services;
using SRM.Services.Api.Courses.Models;

namespace SRM.Services.Api.Courses.Controllers
{

    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    public class LessonController : Controller
    {
        ILessonService lessonService;
        public LessonController(ILessonService lessonService)
        {
            this.lessonService = lessonService;
        }

        [HttpGet("GetLessonSessions")]
        public IActionResult GetLessonSessions(SearchStudentLessonSessionsRequest request)
        {
            var lessonSessions = lessonService.GetLessonSessions(request.Id, request.SearchDate);
            var response = LessonModeller.ToLessonSessionsResponse(lessonSessions);
            return Ok(response);
        }
    }
}