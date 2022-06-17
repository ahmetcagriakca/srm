using Fix.Data;

namespace SRM.Data.Models.Individuals.Parameters
{
    public class ObstacleType : ParameterEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
