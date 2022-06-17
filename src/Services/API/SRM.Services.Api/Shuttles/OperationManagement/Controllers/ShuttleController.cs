using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SRM.Data.Models.Shuttles;
using SRM.Domain.Shuttles.OperationManagement.Services;
using SRM.Services.Api.BaseModel;
using SRM.Services.Api.Shuttles.OperationManagement.Models;
using SRM.Services.Api.Shuttles.OperationManagement.Models.StudentOperationLessonRelation;
using System;

namespace SRM.Services.Api.Shuttles.OperationManagement.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    [ApiController]
    public class ShuttleController : ControllerBase
    {
        private readonly IShuttleService shuttleService;

        public ShuttleController(IShuttleService shuttleService)
        {
            this.shuttleService = shuttleService;
        }

        [HttpGet("CreateWeeklyShuttleOperation")]
        public IActionResult CreateWeeklyShuttleOperation(DateTime weekStartTime)
        {
            bool result = shuttleService.CreateWeeklyShuttleOperation(weekStartTime);
            return Ok(new BaseServiceResponse());
        }

        [HttpGet("CreateShuttleOperationByTemplateId")]
        public IActionResult CreateShuttleOperationByTemplateId(int shuttleTemplateId)
        {
            bool result = shuttleService.CreateShuttleOperationByTemplateId(shuttleTemplateId);
            return Ok(new BaseServiceResponse());
        }

        [HttpPut("SetStudentShuttleOperationStatus")]
        public IActionResult SetStudentShuttleOperationStatus(SetStudentShuttleOperationStatusRequest request)
        {
            shuttleService.SetStudentShuttleOperationStatus(request.StudentShuttleOperationId, request.StudentOperasionStatus);

            if (request.StudentOperasionStatus == ShuttleStudentOperationStatus.Come)
                shuttleService.SetStudentOperationLocation(request.StudentShuttleOperationId, request.LocationX, request.LocationY);

            return Ok(new BaseServiceResponse());
        }

        // GET: api/Shuttle
        [HttpGet("GetStudentShuttleCallList")]
        public IActionResult GetStudentShuttleCallList(DateTime date)
        {
            var shuttleOperationList = shuttleService.GetStudentOperationAdvicesByDate(date);
            var response = shuttleOperationList.GetStudentShutleCallListResponse();

            return Ok(response);
        }

        [HttpGet("GetStudentOperationAdvicesByShuttleOperationId/{stuttleOperationId}")]
        public IActionResult GetStudentOperationAdvicesByShuttleOperationId(long stuttleOperationId)
        {
            var shuttleOperationList = shuttleService.GetStudentOperationAdvicesByShuttleOperationId(stuttleOperationId);
            var response = shuttleOperationList.GetStudentShutleCallListResponse();
            return Ok(response);
        }
        


        [HttpGet("GetStudentShuttleOperationById")]

        public IActionResult GetStudentShuttleOperationById(int id)
        {
            var shuttleOperation = shuttleService.GetStudentShuttleOperationById(id);
            var response = shuttleOperation.GetShuttleOperationWithStudentsResponse();
            return Ok(response);
        }

        [HttpGet ("GetStudentOperationListByDate")]

        public IActionResult GetStudentOperationListByDate(DateTime date)
        {
            var shuttleOperationList = shuttleService.GetStudentOperationListByDate(date);
            var response = shuttleOperationList.GetShuttleOperationListWithStudentsResponse();
            return Ok(response);
        }

        [HttpGet("GetStudentOperationListByShuttleOperationId")]

        public IActionResult GetStudentOperationListByShuttleOperationId(long shuttleOperationId)
        {
            var studentList = shuttleService.GetStudentOperationListByShuttleOperationId(shuttleOperationId);
            var response = studentList.GetStudentOperationListResponse();
            return Ok(response);
        }

        [HttpGet("GetShuttleOperationById")]
        public IActionResult GetShuttleOperationById(int operationId)
        {
            var shuttleOperationList = shuttleService.GetShuttleOperationById(operationId);
            var response = shuttleOperationList.GetShuttleOperationResponse();
            return Ok(response);
        }

        [HttpGet ("GetShuttleOperationListByDate")]
        public IActionResult GetShuttleOperationListByDate (DateTime date) {
            var shuttleOperationList = shuttleService.GetShuttleOperationListByDate (date);
            var response = shuttleOperationList.GetShuttleOperationListResponse ();
            return Ok (response);
        }

        [HttpGet("GetShuttleOperationListByDateForDriver")]
        public IActionResult GetShuttleOperationListByDateForDriver(DateTime date)
        {
            var shuttleOperationList = shuttleService.GetShuttleOperationListByDateForDriver(date);
            var response = shuttleOperationList.GetShuttleOperationListResponse();
            return Ok(response);

        }

        [HttpGet("GetStudentShuttleDailyCallListByStudent")]
        public IActionResult GetStudentOperationAdvicesByDateAndStudent(DateTime date, long studentId)
        {
            var shuttleOperationList = shuttleService.GetStudentOperationAdvicesByDateAndStudent(date, studentId);
            var response = shuttleOperationList.GetStudentShutleCallListResponse();
            return Ok(response);
        }

        [HttpGet("GetStudentShuttleAdviceById/{adviceId}")]
        public IActionResult GetStudentShuttleAdviceById(int adviceId)
        {
            var shuttleOperation = shuttleService.GetStudentShuttleAdviceById(adviceId);
            var response = shuttleOperation.GetStudentShuttleAdvice();
            return Ok(response);
        }

        [HttpPost ("CreateAdvice")]
        public IActionResult CreateAdvice (DateTime date, long shuttleOperationId) {
            shuttleService.CreateAdvice (date, shuttleOperationId);
            return Ok (true);

        }

        [HttpPut("SetShuttleOperationStatus")]
        public IActionResult SetShuttleOperationStatus([FromBody] SetShuttleOperationStatusRequest request)
        //  long shuttleOparationId, int status)
        {
            shuttleService.SetShuttleOperationStatus(request.ShuttleOperationId, request.Status);
            return Ok(new BaseServiceResponse());
        }

        [HttpPut("ChangeStudentOperationByAvaibleTime/{studentAvailableTimeId}")]
        public IActionResult ChangeStudentOperationByAvaibleTime(long studentAvailableTimeId)
        {
            shuttleService.ChangeStudentOperationByAvaibleTime(studentAvailableTimeId);
            return Ok(new BaseServiceResponse());
        }

        [HttpPost("CreateCustomShuttleOperation")]
        public IActionResult CreateCustomShuttleOperation([FromBody] CreateCustomShuttleOperationRequest request)
        {
            var operationId = shuttleService.CreateCustomShuttleOperation(request.OperationDate, request.RegionId, request.StudentServiceId);

            return Ok(new BaseServiceResponse()
            {
                ResultValue = new
                {
                    operationId = operationId

                }
            });
        }

        [HttpPost("CreateCustomStudentOperation")]
        public IActionResult CreateCustomStudentOperation([FromBody] CreateCustomStudentOperationRequest request)
        {
            shuttleService.CreateCustomStudentOperation(request.StudentId, request.ShuttleOperationId, request.LessonCount);
            return Ok(new BaseServiceResponse());
        }

        [HttpPut("SetStudentOperastionLessonsCount")]
        public IActionResult SetStudentOperastionLessonsCount(SetStudentOperastionLessonsCountRequest request)
        {
            shuttleService.SetStudentOperastionLessonsCount(request.ShuttleStudentOperationId, request.ComplatedLessonCount);
            return Ok(new BaseServiceResponse());
        }

        [HttpPut("SetStudentShuttleOperationStatusForDriver")]
        public IActionResult SetStudentShuttleOperationStatusForDriver(SetStudentShuttleOperationStatusRequest request)
        {
            shuttleService.SetStudentShuttleOperationStatusForDriver(request.StudentShuttleOperationId, request.StudentOperasionStatus, request.LocationX, request.LocationY);

            return Ok(new BaseServiceResponse());
        }

        [HttpGet("GetInstructorStudents")]
        public IActionResult GetInstructorStudents(DateTime date)
        {
            var result = shuttleService.GetInstructorStudents(date);
            var response = result.GetInstructorStudentsResponse();

            return Ok(response);
        }

        [HttpGet("GetStudentOperations")]
        public IActionResult GetStudentOperations(long studentId)
        {
            var result = shuttleService.GetStudentOperations(studentId);
            var response = result.GetStudentOperations();

            return Ok(response);
        }


        [HttpGet("GetShuttleOperationStudentLocations")]
        public IActionResult GetShuttleOperationStudentLocations(long shuttleOperationId)
        {
            var result = shuttleService.GetShuttleOperationStudentLocations(shuttleOperationId);
            var response = result.GetShuttleOperationStudentLocations();

            return Ok(response);
        }
    }
}