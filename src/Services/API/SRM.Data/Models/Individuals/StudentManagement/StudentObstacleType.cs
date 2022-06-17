using Fix.Data;
using SRM.Data.Models.Individuals.Parameters;

namespace SRM.Data.Models.Individuals.StudentManagement
{

    public class StudentObstacleType : FixEntity<int>
    {
        public Student Student { get; set; }
        public ObstacleType ObstacleType { get; set; }
    }
}
