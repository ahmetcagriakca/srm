using Fix.Data;
using SRM.Data.Models.Courses;
using System.Collections.Generic;

namespace SRM.Data.Models.Individuals.StudentManagement
{
    public class StudentLesson : FixEntity<long>
    {
        public Student Student { get; set; }
        public Lesson Lesson { get; set; }
        public ICollection<StudentLessonSession> LessonSessions { get; set; }

    }
}
