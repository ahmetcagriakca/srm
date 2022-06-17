using Fix.Data;

namespace IdentityServer.Models
{

    public class UserInRole : AuthEntity<int>
    {
        public long? UserId { get; set; }
        public int? RoleId { get; set; }
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
