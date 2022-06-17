using SRM.Data.Models.HospitalAppointment;
using SRM.Data.Models.Individuals.Parameters;
using SRM.Services.Api.BaseModel;
using System.Collections.Generic;
using System.Linq;

namespace SRM.Services.Api.HospitalAppointment.Models
{

    public class SaveAppointmentStudentRequest
    {
        public string NameSurname { get; set; }
        public string IdentityNumber { get; set; }
        public string MHRSPassword { get; set; }
        public int Order { get; set; }
        public List<int> HospitalIds { get; set; }
        public int InstitutionId { get; set; }
    }

    public class UpdateAppointmentStudentStatusRequest
    {
        public int StudentId { get; set; }
        public ProcessStatus ProcessStatus { get; set; }
    }

    public class SaveHospitalAppointmentInstitutionRequest
    {
        public string InstitutionName { get; set; }
        public int PriorityOrder { get; set; }
        public bool IsActive { get; set; }

    }


    public static class HospitalAppointmentModeller
    {
        internal static AppointmentStudent SaveAppointmentToModel(this SaveAppointmentStudentRequest request)
        {
            var entity = new AppointmentStudent
            {
                NameSurname = request.NameSurname,
                IdentityNumber = request.IdentityNumber,
                MHRSPassword = request.MHRSPassword,
                Order = request.Order
            };
            return entity;
        }

        internal static HospitalAppointmentInstitution SaveHospitalAppointmentInstitutionToModel(this SaveHospitalAppointmentInstitutionRequest request)
        {
            var entity = new HospitalAppointmentInstitution
            {
                InstitutionName = request.InstitutionName,
                PriorityOrder = request.PriorityOrder,
                IsActive = request.IsActive
            };
            return entity;
        }

        internal static object GetStudentShutleCallListResponse(this IEnumerable<Hospital> hospitals)
        {
            var result = from h in hospitals
                         select new
                         {
                             h.Id,
                             h.Name

                         };

            return new BaseServiceResponse()
            {
                ResultValue = result
            };
        }

        internal static object GetNotStartedStudentsByHospitalResponse(this IEnumerable<AppointmentStudent> students, int hospitalId)
        {
            var result = from h in students.OrderBy(x => x.HospitalAppointmentInstitution.PriorityOrder).ThenBy(x => x.Order)
                         select new
                         {
                             StudentId = h.Id,
                             h.NameSurname,
                             h.IdentityNumber,
                             h.MHRSPassword,
                             h.ProcessStatus,
                             h.Order,
                             OtherHospitals = from a in h.AppointmentStudentHospitalRelations
                                              where a.Hospital.Id != hospitalId
                                              select new
                                              {
                                                  Id = a.Hospital.Id,
                                                  Name = a.Hospital.Name
                                              }

                         };

            return new BaseServiceResponse()
            {
                ResultValue = result
            };
        }
    }
}