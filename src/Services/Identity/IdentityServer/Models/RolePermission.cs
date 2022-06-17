using Fix.Data;

namespace IdentityServer.Models
{
    public class RolePermission : AuthEntity<int>
    {
        public Role Role { get; set; }
        public string Permission { get; set; }
    }
}
