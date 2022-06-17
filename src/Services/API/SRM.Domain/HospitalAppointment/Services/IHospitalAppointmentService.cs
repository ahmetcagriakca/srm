using Fix;
using SRM.Data.Models.HospitalAppointment;
using SRM.Data.Models.Individuals.Parameters;
using System.Collections.Generic;

namespace SRM.Domain.HospitalAppointment.Services
{
    public interface IHospitalAppointmentService : IDependency
    {
        IEnumerable<Hospital> GetHospitalContainsActiveStudent();
        IEnumerable<AppointmentStudent> GetNotStartedStudentsByHospital(int hospitalId);

        void SaveHospitalAppointmentInstitution(HospitalAppointmentInstitution hospitalAppointmentInstitution);
        void SaveAppointmentStudent(AppointmentStudent student, int institutionId, List<int> hospitalId);
        void UpdateAppointmentStudentStatus(int studentId, ProcessStatus processStatus);
    }
}