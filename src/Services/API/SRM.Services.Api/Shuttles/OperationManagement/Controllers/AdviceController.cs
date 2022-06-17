using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SRM.Domain.Shuttles.OperationManagement.Services;
using SRM.Services.Api.BaseModel;
using SRM.Services.Api.Shuttles.OperationManagement.Models.StudentOperationLessonRelation;

namespace SRM.Services.Api.Shuttles.OperationManagement.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdviceController : ControllerBase
    {
        private readonly IShuttleAdviceService adviceService;

        public AdviceController(IShuttleAdviceService _adviceService)
        {
            adviceService = _adviceService;
        }

        [HttpPost("GetAdvices")]
        public IActionResult GetAdvices([FromBody] GetAdviceRequest request)
        {
            var result = adviceService.GetAdvices(request);
            return Ok(new BaseServiceResponse()
            {
                ResultValue = new
                {
                    pageNumber = request.PageNumber,
                    pageSize = request.PageSize,
                    advices = result.Advices,
                    TotalCount = result.TotalCount
                }
            });
        }
        [HttpPost("SetAdviceToOperation")]
        public IActionResult SetAdviceToOperation(SetAdviceToOperationRequest request)
        {
            adviceService.SetAdviceToOperation(request.StudentId, request.ShuttleOperationId);

            return Ok(new BaseServiceResponse());

        }




    }


}