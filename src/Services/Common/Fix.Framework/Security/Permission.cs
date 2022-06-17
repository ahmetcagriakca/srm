using System.Collections.Generic;

namespace Fix.Security
{
    public class Permission
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public IEnumerable<Permission> ImpliedBy { get; set; }
    }

}
