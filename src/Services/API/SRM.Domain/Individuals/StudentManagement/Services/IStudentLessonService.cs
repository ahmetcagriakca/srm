using Fix;
using SRM.Data.Models.Courses;
using System.Collections.Generic;

namespace SRM.Domain.Individuals.StudentManagement.Services
{
    public interface IStudentLessonService : IDependency
    {
        #region Student Lessons
        IEnumerable<Lesson> GetStudentLessons(long studentId);

        Lesson GetStudentLessonById(long studentId, long id);

        void CreateStudentLesson(Lesson studentLesson);

        void UpdateStudentLesson(Lesson studentLesson);

        void DeleteStudentLesson(long studentId, long id);
        #endregion
    }
}
