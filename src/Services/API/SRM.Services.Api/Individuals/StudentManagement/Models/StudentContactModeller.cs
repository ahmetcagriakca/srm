using SRM.Data.Models.Individuals.StudentManagement;
using SRM.Services.Api.BaseModel;
using System.Collections.Generic;
using System.Linq;

namespace SRM.Services.Api.Individuals.StudentManagement.Models
{
    public class CreateStudentContactRequest
    {
        public string Name { get; set; }
        public string Number { get; set; }

    }

    public class UpdateStudentContactRequest : CreateStudentContactRequest
    {
        public long Id { get; set; }
    }
    public static class StudentContactModeller
    {
        public static StudentContact CreateStudentContactRequestToModel(this CreateStudentContactRequest request)
        {

            return new StudentContact
            {
                Name = request.Name,
                Number = request.Number
            };
        }

        public static void UodateStudentContactRequestToModel(this UpdateStudentContactRequest request, ref StudentContact model)
        {
            model.Name = request.Name;
            model.Number = request.Number;
        }

        public static BaseServiceResponse ToGetStudentContactsResponse(IEnumerable<StudentContact> model)
        {
            var results = from contact in model
                          select new
                          {
                              contact.Id,
                              contact.Name,
                              contact.Number
                          };
            return new BaseServiceResponse
            {
                ResultValue = results.ToList()
            };
        }

        public static BaseServiceResponse ToGetStudentContactResponse(StudentContact contact)
        {

            return new BaseServiceResponse
            {
                ResultValue = new
                {
                    contact.Id,
                    contact.Name,
                    contact.Number
                }
            };
        }

    }
}