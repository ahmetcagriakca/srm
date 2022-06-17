using Fix.Data;
using Microsoft.EntityFrameworkCore;
using SRM.Data.Models.Courses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SRM.Domain.Individuals.StudentManagement.Services
{
    public class StudentLessonService : IStudentLessonService
    {
        private readonly IRepository<Lesson> studentLessonRepository;

        public StudentLessonService(
            IRepository<Lesson> studentLessonRepository)
        {
            this.studentLessonRepository = studentLessonRepository ?? throw new ArgumentNullException(nameof(studentLessonRepository));
        }
        #region Student Lessons
        public IEnumerable<Lesson> GetStudentLessons(long studentId)
        {
            var results = studentLessonRepository.Table
                .Include(en => en.Branch)
                .Where(en => en.Id == studentId)/*.Where(en=> (searchDate == null || en.LessonSessions.Any(lsEn => lsEn.StartDate < searchDate) || !en.LessonSessions.Any()))*/;
            return results;
        }

        public Lesson GetStudentLessonById(long studentId, long id)
        {
            //var result = studentLessonRepository.Table
            //	//.Include(en => en.Student)
            //	.FirstOrDefault(en => en.Student.Id == studentId && en.Id == id);
            //return result;
            return null;
        }

        public void CreateStudentLesson(Lesson studentLesson)
        {
            studentLessonRepository.Add(studentLesson);
        }

        public void UpdateStudentLesson(Lesson studentLesson)
        {
            studentLessonRepository.Update(studentLesson);
        }

        public void DeleteStudentLesson(long studentId, long id)
        {
            var studentLesson = GetStudentLessonById(studentId, id);
            studentLessonRepository.Delete(studentLesson);
        }

        #endregion
    }
}
