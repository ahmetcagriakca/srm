using SRM.Data.Models.Times;

namespace SRM.Data.Models.Individuals.InstructorManagement
{
    public class InstructerAvailableTime : AvailableTime
    {
        public Instructor Instructor { get; set; }

    }
}
