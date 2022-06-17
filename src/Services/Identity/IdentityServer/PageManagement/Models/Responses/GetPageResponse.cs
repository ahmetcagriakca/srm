using IdentityServer.Models.PageManagement;
using System.Collections.Generic;

namespace IdentityServer.PageManagement.Models
{
    public class GetPageResponse
    {
        public int Id { get; set; }
        public string Url { get; set; }

        public string Name { get; set; }

        public string ComponentName { get; set; }

        public int? Order { get; set; }

        public string Icon { get; set; }

        public bool ShowOnMenu { get; set; }

        public int? ParentId { get; set; }

        public Page Parent { get; set; }

        public ICollection<Page> Children { get; set; }

        public ICollection<PageRole> PageRoles { get; set; }

        public bool IsParent { get; set; }

        public bool IsActive { get; set; }
    }
}
