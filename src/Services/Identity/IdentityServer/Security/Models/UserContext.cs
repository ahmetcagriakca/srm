using Fix.Security.OpenAuthentication;
using System.Collections.Generic;

namespace IdentityServer.Security.Models
{
    public class ClientContext : IClientContext
    {
        public string Key { get; set; }
        public IEnumerable<string> Permissions { get; set; }
        public long UserId { get; set; }
        public long CompanyId { get; set; }
    }
}
