using Fix.Data;
using Microsoft.EntityFrameworkCore;
using SRM.Data.Models.Individuals.StudentManagement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SRM.Domain.Individuals.StudentManagement.Services
{
    public class StudentReportService : IStudentReportService
    {
        private readonly IRepository<StudentReport> studentReportRepository;

        public StudentReportService(
            IRepository<StudentReport> studentReportRepository)
        {
            this.studentReportRepository = studentReportRepository ?? throw new ArgumentNullException(nameof(studentReportRepository));
        }
        #region Student Reports
        public IEnumerable<StudentReport> GetStudentReports(long studentId)
        {
            var results = studentReportRepository.Table
                .Include(en => en.Student)
                .Include(en => en.GivenHospital)
                .Where(en => en.Student.Id == studentId)/*.Where(en=> (searchDate == null || en.LessonSessions.Any(lsEn => lsEn.StartDate < searchDate) || !en.LessonSessions.Any()))*/;
            return results;
        }

        public StudentReport GetStudentReportById(long studentId, long id)
        {
            var result = studentReportRepository.Table
                .Include(en => en.Student)
                .Include(en => en.GivenHospital)
                .FirstOrDefault(en => en.Student.Id == studentId && en.Id == id);
            return result;
        }

        public void CreateStudentReport(StudentReport studentReport)
        {
            studentReportRepository.Add(studentReport);
        }

        public void UpdateStudentReport(StudentReport studentReport)
        {
            studentReportRepository.Update(studentReport);
        }

        public void DeleteStudentReport(long studentId, long id)
        {
            var studentReport = GetStudentReportById(studentId, id);
            studentReportRepository.Delete(studentReport);
        }

        #endregion
    }
}
