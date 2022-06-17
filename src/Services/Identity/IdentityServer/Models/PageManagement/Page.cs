using Fix.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityServer.Models.PageManagement
{
    public class Page : ApplicationParameterEntity<int>
    {
        public Page()
        {
            PageRoles = new HashSet<PageRole>();
            Children = new HashSet<Page>();
        }

        public string Url { get; set; }

        public string Name { get; set; }

        public string ComponentName { get; set; }

        public int Order { get; set; }

        public string Icon { get; set; }

        public bool ShowOnMenu { get; set; }

        [NotMapped]
        public bool IsParent { get; set; }

        public int? ParentId { get; set; }

        public Page Parent { get; set; }

        public ICollection<Page> Children { get; set; }

        public ICollection<PageRole> PageRoles { get; set; }
    }

    public class PageRole : FixEntity<int>
    {

        public int? PageId { get; set; }
        public int? RoleId { get; set; }
        public virtual Page Page { get; set; }
        public virtual Role Role { get; set; }
    }
}
