using Fix.Data;
using SRM.Data.Models.Courses;

namespace SRM.Data.Models.Individuals.StudentManagement
{
    public class StudentLessonSession : FixEntity<long>
    {
        public StudentLesson StudentLesson { get; set; }
        public LessonSession LessonSession { get; set; }

    }
}
