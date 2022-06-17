using Fix.Data;

namespace SRM.Data.Models.Courses.Parameters
{
    public class CourseTime : ParameterEntity<int>
    {
        public int DayOfWeek { get; set; }

        public int HourOfDay { get; set; }

        public int MinituOfHour { get; set; }

        public int CourseDuration { get; set; }
    }
}
