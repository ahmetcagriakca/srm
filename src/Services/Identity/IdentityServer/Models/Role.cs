using Fix.Data;
using IdentityServer.Models.PageManagement;
using System.Collections.Generic;

namespace IdentityServer.Models
{
    public class Role : AuthEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<PageRole> PageRoles { get; set; }
        public virtual ICollection<UserInRole> UserInRoles { get; set; }
        public virtual ICollection<RolePermission> Permissions { get; set; }
    }
}
