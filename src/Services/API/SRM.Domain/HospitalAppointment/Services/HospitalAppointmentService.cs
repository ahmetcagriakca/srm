using Fix.Data;
using Microsoft.EntityFrameworkCore;
using SRM.Data.Models.HospitalAppointment;
using SRM.Data.Models.Individuals.Parameters;
using System.Collections.Generic;
using System.Linq;

namespace SRM.Domain.HospitalAppointment.Services
{
    public class HospitalAppointmentService : IHospitalAppointmentService
    {

        private readonly IRepository<HospitalAppointmentInstitution> institutionRepository;
        private readonly IRepository<AppointmentStudent> studentRepository;
        private readonly IRepository<Hospital> hospitalRepository;

        public HospitalAppointmentService(
            IRepository<HospitalAppointmentInstitution> _appointmentInstitutionRepository,
            IRepository<AppointmentStudent> _appointmentStudentRepository,
            IRepository<Hospital> _hospitalRepository
        )
        {
            institutionRepository = _appointmentInstitutionRepository;
            studentRepository = _appointmentStudentRepository;
            hospitalRepository = _hospitalRepository;
        }

        public IEnumerable<Hospital> GetHospitalContainsActiveStudent()
        {

            var hospital = hospitalRepository.Table.Include(x => x.AppointmentStudentHospitalRelations)//.ThenInclude(x => x)            
            .Where(x => x.IsActive
            && x.AppointmentStudentHospitalRelations.Any(y => y.AppointmentStudent.ProcessStatus == ProcessStatus.NotStart
             && y.AppointmentStudent.HospitalAppointmentInstitution.IsActive
            ));
            return hospital;

        }

        public void SaveHospitalAppointmentInstitution(HospitalAppointmentInstitution hospitalAppointmentInstitution)
        {
            institutionRepository.Add(hospitalAppointmentInstitution);
        }

        /// <summary>
        /// Öğrenci randevu kaydı olusturma
        /// </summary>
        /// <param name="student"></param>
        public void SaveAppointmentStudent(AppointmentStudent student, int institutionId, List<int> hospitalIds)
        {
            var institution = institutionRepository.FindBy(institutionId);
            student.HospitalAppointmentInstitution = institution;

            student.AppointmentStudentHospitalRelations = new List<AppointmentStudentHospitalRelation>();
            foreach (var hospitalId in hospitalIds)
            {
                var hospital = hospitalRepository.FindBy(hospitalId);
                var relation = new AppointmentStudentHospitalRelation
                {
                    Hospital = hospital
                };
                student.AppointmentStudentHospitalRelations.Add(relation);
            }

            studentRepository.Add(student);
        }

        public void UpdateAppointmentStudentStatus(int studentId, ProcessStatus processStatus)
        {
            var student = studentRepository.FindBy(studentId);
            student.ProcessStatus = processStatus;

            studentRepository.Update(student);
        }

        /// <summary>
        /// Öğrencileri ilişkilerle getir
        /// </summary>
        /// <returns></returns>
        private IQueryable<AppointmentStudent> GetAppointmentStudentsWithRelations()
        {
            var students = studentRepository.Table.Include(x => x.HospitalAppointmentInstitution).Include(x => x.AppointmentStudentHospitalRelations).ThenInclude(x => x.Hospital);

            return students;
        }

        /// <summary>
        /// Başlamamış durumdaki öğrencileri getir
        /// </summary>
        /// <returns></returns>
        private IQueryable<AppointmentStudent> GetNotStartedAppointmentStudents()
        {
            var students = GetAppointmentStudentsWithRelations().Where(x => x.ProcessStatus == ProcessStatus.NotStart && x.HospitalAppointmentInstitution.IsActive);

            return students;
        }

        /// <summary>
        /// Hastaneye göre başlamamış durumdaki öğrecileri getir
        /// </summary>
        /// <param name="hospitalId"></param>
        /// <returns></returns>
        public IEnumerable<AppointmentStudent> GetNotStartedStudentsByHospital(int hospitalId)
        {
            var students = GetNotStartedAppointmentStudents().Where(x => x.AppointmentStudentHospitalRelations.Any(y => y.Hospital.Id == hospitalId));
            return students;

        }




    }


}