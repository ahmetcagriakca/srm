using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SRM.Domain.Reports.Services;
using SRM.Services.Api.BaseModel;
using System;

namespace SRM.Services.Api.Reports.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : Controller
    {
        private readonly IShuttleReportService _shuttleReportService;
        public ReportController(
            IShuttleReportService shuttleReportService
            )
        {
            _shuttleReportService = shuttleReportService;
        }

        [HttpGet("GetStudentJoinStatus")]
        public IActionResult GetStudentJoinStatus(DateTime startDate, DateTime endDate)
        {
            var result = _shuttleReportService.GetStudentJoinStatus(startDate, endDate);
            return Ok(new BaseServiceResponse { ResultValue = result });
        }


    }
}