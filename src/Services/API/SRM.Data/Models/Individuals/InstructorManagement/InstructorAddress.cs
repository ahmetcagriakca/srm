using Fix.Data;
using SRM.Data.Models.Application;

namespace SRM.Data.Models.Individuals.InstructorManagement
{
    public class InstructorAddress : FixEntity<long>
    {
        public Instructor Instructor { get; set; }

        public Address Address { get; set; }
    }
}
