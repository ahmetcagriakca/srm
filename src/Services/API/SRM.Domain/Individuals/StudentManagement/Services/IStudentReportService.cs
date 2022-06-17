using Fix;
using SRM.Data.Models.Individuals.StudentManagement;
using System.Collections.Generic;

namespace SRM.Domain.Individuals.StudentManagement.Services
{
    public interface IStudentReportService : IDependency
    {
        #region Student Reports
        IEnumerable<StudentReport> GetStudentReports(long studentId);

        StudentReport GetStudentReportById(long studentId, long id);

        void CreateStudentReport(StudentReport studentReport);

        void UpdateStudentReport(StudentReport studentReport);

        void DeleteStudentReport(long studentId, long id);
        #endregion
    }
}
