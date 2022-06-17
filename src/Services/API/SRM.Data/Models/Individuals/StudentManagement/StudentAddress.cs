using Fix.Data;
using SRM.Data.Models.Application;

namespace SRM.Data.Models.Individuals.StudentManagement
{
    public class StudentAddress : FixEntity<long>
    {
        public Student Student { get; set; }
        public Address Address { get; set; }
    }
}
