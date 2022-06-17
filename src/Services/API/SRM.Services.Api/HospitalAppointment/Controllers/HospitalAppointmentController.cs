using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SRM.Domain.HospitalAppointment.Services;
using SRM.Services.Api.BaseModel;
using SRM.Services.Api.HospitalAppointment.Models;

namespace SRM.Services.Api.HospitalAppointment.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalAppointmentController : ControllerBase
    {
        private readonly IHospitalAppointmentService appointmentService;
        public HospitalAppointmentController(IHospitalAppointmentService _appointmentService)
        {
            appointmentService = _appointmentService;
        }

        [HttpPost("SaveHospitalAppointmentInstitution")]
        public IActionResult SaveHospitalAppointmentInstitution(SaveHospitalAppointmentInstitutionRequest request)
        {
            appointmentService.SaveHospitalAppointmentInstitution(request.SaveHospitalAppointmentInstitutionToModel());
            return Ok(new BaseServiceResponse());
        }

        [HttpPost("SaveAppointmentStudent")]
        public IActionResult SaveAppointmentStudent(SaveAppointmentStudentRequest request)
        {
            appointmentService.SaveAppointmentStudent(request.SaveAppointmentToModel(), request.InstitutionId, request.HospitalIds);
            return Ok(new BaseServiceResponse());
        }

        [HttpPost("UpdateAppointmentStudentStatus")]
        public IActionResult UpdateAppointmentStudentStatus(UpdateAppointmentStudentStatusRequest request)
        {
            appointmentService.UpdateAppointmentStudentStatus(request.StudentId, request.ProcessStatus);
            return Ok(new BaseServiceResponse());
        }

        [HttpGet("GetHospitalContainsActiveStudent")]
        public IActionResult GetHospitalContainsActiveStudent()
        {
            var result = appointmentService.GetHospitalContainsActiveStudent();
            return Ok(result.GetStudentShutleCallListResponse());
        }

        [HttpGet("GetNotStartedStudentsByHospital/{hospitalId}")]
        public IActionResult GetNotStartedStudentsByHospital(int hospitalId)
        {
            var result = appointmentService.GetNotStartedStudentsByHospital(hospitalId).GetNotStartedStudentsByHospitalResponse(hospitalId);
            return Ok(result);
        }
    }
}