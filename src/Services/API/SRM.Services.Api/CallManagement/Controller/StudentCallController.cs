using Fix;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SRM.Domain.Shuttles.OperationManagement.Services;
using SRM.Services.Api.BaseModel;
using SRM.Services.Api.CallManagement.Controller.Models;

namespace SRM.Services.Api.CallManagement.Controller
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentCallController : ControllerBase
    {
        private readonly IStudentCallService studentCallService;
        private readonly IWorkContext workContext;


        public StudentCallController(IStudentCallService _studentCallService,
        IWorkContext _workContext)
        {
            studentCallService = _studentCallService;
            workContext = _workContext;
        }

        [HttpPost("SaveStudentCall")]
        public IActionResult SaveStudentCall(StudentCallModeller.SaveStudentCallRequest request)
        {
            studentCallService.SaveStudentPhoneCall(request.SaveStudentCallRequestToModel(), request.StudentId, request.OperationId);
            return Ok(new BaseServiceResponse());
        }

        [HttpGet("GetStudentPhoneCalls/{studentId}")]
        public IActionResult GetStudentPhoneCalls(long studentId)
        {
            var result = studentCallService.GetStudentsCalls(studentId).ToGetStudentPhoneCallByUser();
            return Ok(result);
        }


        [HttpGet("GetStudentPhoneCallsByCurrentUser")]
        public IActionResult GetStudentPhoneCallsByCurrentUser()
        {
            var userId = workContext.AuthenticationProvider.GetUserId();

            var result = studentCallService.GetStudentCallsByUser(userId).ToGetStudentPhoneCallByUser();
            return Ok(result);

        }

        [HttpGet("GetStudentPhoneCallByUserId/{userId}")]
        public IActionResult GetStudentPhoneCallByUserId(long userId)
        {
            var result = studentCallService.GetStudentCallsByUser(userId).ToGetStudentPhoneCallByUser();
            return Ok(result);
        }

        [HttpGet("GetStudentsCallsByUserRoleType/{studentId}")]
        public IActionResult GetStudentsCallsByUserRoleType(long studentId, string userRole)
        {
            var result = studentCallService.GetStudentsCallsByUserRoleType(studentId, userRole).ToGetStudentPhoneCallByUser();
            return Ok(result);
        }
    }
}