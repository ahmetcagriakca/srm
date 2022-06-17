using Fix.Data;
using System.Collections.Generic;

namespace IdentityServer.Models
{
    public class Company : AuthEntity<int>
    {
        public string Name { get; set; }
        public string SystemName { get; set; }
        public string ApiUrl { get; set; }
        public ICollection<User> Users { get; set; }

    }
}
