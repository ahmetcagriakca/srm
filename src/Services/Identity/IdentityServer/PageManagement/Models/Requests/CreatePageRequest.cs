using System.Collections.Generic;

namespace IdentityServer.PageManagement.Models
{
    public class CreatePageRequest
    {

        public string Url { get; set; }

        public string Name { get; set; }

        public string ComponentName { get; set; }

        public int Order { get; set; }

        public string Icon { get; set; }

        public bool ShowOnMenu { get; set; }

        public int? ParentId { get; set; }

        public List<int> PageRoleIds { get; set; }

        public bool IsActive { get; set; }
    }
}
