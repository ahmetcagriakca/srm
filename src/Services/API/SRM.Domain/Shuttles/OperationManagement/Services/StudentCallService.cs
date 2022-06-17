using System.Collections.Generic;
using System.Linq;
using Fix;
using Fix.Data;
using Microsoft.EntityFrameworkCore;
using SRM.Data.Models.CallManagement;
using SRM.Data.Models.Individuals.StudentManagement;
using SRM.Data.Models.Shuttles;
using SRM.Domain.Shuttles.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace SRM.Domain.Shuttles.OperationManagement.Services
{
    public class StudentCallService : IStudentCallService
    {
        private readonly IRepository<StudentPhoneCall> studentPhoneCallRepository;
        private readonly IRepository<Student> studentRepository;
        private readonly IShuttleService shuttleService;

        public StudentCallService(IRepository<StudentPhoneCall> studentPhoneCallRepository,
        IRepository<Student> studentRepository,
        IShuttleService shuttleService)
        {
            this.studentPhoneCallRepository = studentPhoneCallRepository;
            this.studentRepository = studentRepository;
            this.shuttleService = shuttleService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="phoneCall"></param>
        /// <param name="studentId"></param>
        /// <param name="operationId"></param>
        public void SaveStudentPhoneCall(StudentPhoneCall phoneCall, long studentId, long operationId)
        {
            var student = studentRepository.FindBy(studentId);
            if (student == null)
                throw new StudentCallException("Öğrenci bilgisi bulunamadı.");
            var shuttleStudentOperation = new ShuttleStudentOperation();
            switch (phoneCall.CallType)
            {
                case CallType.Operation:
                    shuttleStudentOperation = shuttleService.GetShuttleStudentOperationByStudent(operationId, studentId);
                    if (shuttleStudentOperation == null)
                        throw new StudentCallException("Öğrenci Servis Operasyon bilgisi bulunamadı.");
                    phoneCall.ShuttleStudentOperation = shuttleStudentOperation;
                    break;

                case CallType.Advice:
                    if (phoneCall.StudentAnswer == StudentAnswer.StudentWillCome)
                    {
                        shuttleStudentOperation = shuttleService.SetAdviceToOperation(operationId,
                          studentId,
                          phoneCall.Description);
                        phoneCall.ShuttleStudentOperation = shuttleStudentOperation;
                    }
                    break;
                default:
                    break;
            }

            phoneCall.Student = student;
            studentPhoneCallRepository.Add(phoneCall);
        }

        public IQueryable<StudentPhoneCall> GetStudentsCallsWithRelation()
        {
            return studentPhoneCallRepository.Table.Include(x => x.Student)
            .Include(x => x.ShuttleStudentOperation);
        }
        public IEnumerable<StudentPhoneCall> GetStudentsCalls(long studentId)
        {
            return GetStudentsCallsWithRelation().Where(x => x.Student.Id == studentId)
            .OrderByDescending(x => x.CreatedOn);
        }

        public IEnumerable<StudentPhoneCall> GetStudentCallsByUser(long userId)
        {
            var calls = GetStudentsCallsWithRelation();

            return calls
            //.Join(users, call => call.CreatedBy, user => user.Id, (call, user) => new { call, user })
            .Where(x => x.CreatedBy == userId)
            .Select(x => x).OrderByDescending(x => x.CreatedBy);
        }

        public IEnumerable<StudentPhoneCall> GetStudentsCallsByUserRoleType(long studentId, string userRole)
        {
            var calls = GetStudentsCallsWithRelation();

            return calls
                      //.Join(users, call => call.CreatedBy, user => user.Id, (call, user) => new { call, user })
                      .Where(x => x.Student.Id == studentId)
            .Select(x => x).OrderByDescending(x => x.CreatedBy);
        }

    }
}