using Fix.Data;
using System.Collections.Generic;

namespace IdentityServer.Models
{

    public class User : AuthEntity<long>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public int? CompanyId { get; set; }
        public Company Company { get; set; }
        public ICollection<UserInRole> UserInRoles { get; set; }
        public ICollection<UserSession> UserSessions { get; set; }
    }
}
