using SRM.Data.Models.CallManagement;
using SRM.Services.Api.BaseModel;
using System.Collections.Generic;
using System.Linq;

namespace SRM.Services.Api.CallManagement.Controller.Models
{
    public static class StudentCallModeller
    {
        public class SaveStudentCallRequest
        {
            public long StudentId { get; set; }
            public string Description { get; set; }
            public long OperationId { get; set; }
            public CallType CallType { get; set; }
            public StudentAnswer StudentAnswer { get; set; }

        }

        public static StudentPhoneCall SaveStudentCallRequestToModel(this SaveStudentCallRequest request)
        {
            var model = new StudentPhoneCall
            {
                Description = request.Description,
                CallType = request.CallType,
                StudentAnswer = request.StudentAnswer
            };
            return model;

        }

        public static BaseServiceResponse ToGetStudentPhoneCallByUser(this IEnumerable<StudentPhoneCall> entities)
        {

            var responseEntities = from entity in entities
                                   select new
                                   {
                                       Student = new
                                       {
                                           Id = entity.Student.Id,
                                           Name = entity.Student.Name,
                                           Surname = entity.Student.Surname
                                       },
                                       StudentPhoneCall = new
                                       {
                                           entity.Id,
                                           entity.Description,
                                           entity.CallType,
                                           OperationId = entity.ShuttleStudentOperation?.Id,
                                           entity.CreatedBy,
                                           entity.CreatedOn,
                                       }
                                   };

            return new BaseServiceResponse
            {
                ResultValue = responseEntities.ToList()
            };
        }
    }
}