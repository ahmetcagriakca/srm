using Microsoft.AspNetCore.Mvc;
using SRM.Domain.Shuttles.OperationManagement.Services;
using SRM.Services.Api.BaseModel;

namespace SRM.Services.Api.Shuttles.OperationManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataMigrationController : ControllerBase
    {
        private readonly IOperationMigrationService migrationService;
        public DataMigrationController(IOperationMigrationService _migrationService)
        {
            migrationService = _migrationService;
        }

        [HttpGet("LessonRealationAddMigration")]
        //[FixAuthorization(Permission = AccountPermissions.REGISTER_USER)]//TODO:Role değiştirebilirz
        public IActionResult LessonRealationAddMigration()
        {
            migrationService.LessonRealationAddMigration();
            return Ok(new BaseServiceResponse());
        }

        [HttpGet("TemplateAddLessonCount")]
        //[FixAuthorization(Permission = AccountPermissions.REGISTER_USER)]
        public IActionResult TemplateAddLessonCount()
        {
            migrationService.TemplateAddLessonCount();
            return Ok(new BaseServiceResponse());
        }

        // [HttpGet("OneridenGelenlereDersSayisiEkleme")]
        // [FixAuthorization(Permission = AccountPermissions.REGISTER_USER)]
        // public IActionResult OneridenGelenlereDersSayisiEkleme()
        // {
        //     migrationService.OneridenGelenlereDersSayisiEkleme();
        //     return Ok(new BaseServiceResponse());
        // }


        [HttpGet("SetNewStudentOperationStatus")]
        //[FixAuthorization(Permission = AccountPermissions.REGISTER_USER)]
        public IActionResult SetNewStudentOperationStatus()
        {
            migrationService.SetNewStudentOperationStatus();
            return Ok(new BaseServiceResponse());
        }

        [HttpGet("SetStudentNewContacts")]
        //[FixAuthorization(Permission = AccountPermissions.REGISTER_USER)]
        public IActionResult SetStudentNewContacts()
        {
            migrationService.SetStudentNewContacts();
            return Ok(new BaseServiceResponse());
        }

        [HttpGet("OperationLocationMigration")]
        //[FixAuthorization(Permission = AccountPermissions.REGISTER_USER)]
        public IActionResult OperationLocationMigration()
        {
            migrationService.OperationLocationMigration();
            return Ok(new BaseServiceResponse());
        }
    }
}