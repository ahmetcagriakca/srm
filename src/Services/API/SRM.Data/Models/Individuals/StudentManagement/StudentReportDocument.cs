using Fix.Data;
using SRM.Data.Models.Application;

namespace SRM.Data.Models.Individuals.StudentManagement
{
    public class StudentReportDocument : FixEntity<int>
    {
        public Document Document { get; set; }
        public StudentReport StudentReport { get; set; }
    }
}
