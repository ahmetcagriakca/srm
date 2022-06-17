using SRM.Data.Models.Times;

namespace SRM.Data.Models.Individuals.StudentManagement
{
    public class StudentAvailableTime : AvailableTime
    {
        public Student Student { get; set; }
    }
}
