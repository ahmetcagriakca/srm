using Fix;
using SRM.Data.Models.CallManagement;
using System.Collections.Generic;

namespace SRM.Domain.Shuttles.OperationManagement.Services
{
    public interface IStudentCallService : IDependency
    {
        void SaveStudentPhoneCall(StudentPhoneCall phoneCall, long studentId, long operationId);
        IEnumerable<StudentPhoneCall> GetStudentsCalls(long studentId);
        IEnumerable<StudentPhoneCall> GetStudentCallsByUser(long userId);

        IEnumerable<StudentPhoneCall> GetStudentsCallsByUserRoleType(long studentId, string userRole);

    }

}