using Fix.Data;

namespace SRM.Data.Models.Parameters
{
    public class ApplicationParameter : ParameterEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
    }
}
