using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SRM.Domain.Dashboard.Services;
using SRM.Services.Api.BaseModel;
using System;

namespace SRM.Services.Api.Dashboard.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        /// <summary>
        /// Verilen güne ait servisteki planlanan ders sayısı üretilen öneri sayısının gösterilmesi.
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [HttpGet("GetDailyLessonStatusStatistics")]
        public IActionResult GetDailyLessonStatusStatistics(DateTime startDate, DateTime endDate)
        {
            var result = _dashboardService.GetDailyLessonStatusStatistics(startDate, endDate);
            return Ok(new BaseServiceResponse()
            {
                ResultValue = result
            });
        }

        /// <summary>
        /// Verilen güne ait servise gelen gelmeyen öğrenci durumu
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [HttpGet("GetComingStatusStatistics")]
        public IActionResult GetComingStatusStatistics(DateTime startDate, DateTime endDate)
        {
            var result = _dashboardService.GetComingStatusStatistics(startDate, endDate);
            return Ok(new BaseServiceResponse()
            {
                ResultValue = result
            });
        }
    }
}
