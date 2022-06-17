using IdentityServer.Models.PageManagement;
using System.Collections.Generic;

namespace IdentityServer.PageManagement.Models
{
    public class SearchPageResponse
    {
        public int Id { get; set; }
        public string Url { get; set; }

        public string Name { get; set; }

        public string ComponentName { get; set; }

        public int? Order { get; set; }

        public string Icon { get; set; }

        public bool ShowOnMenu { get; set; }

        public Page Parent { get; set; }

        public List<PageRole> PageRoles { get; set; }

        public bool IsActive { get; set; }
    }
}
