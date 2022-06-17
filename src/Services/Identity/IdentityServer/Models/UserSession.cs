
using Fix.Data;
using System;

namespace IdentityServer.Models
{
    public class UserSession : AuthEntity<Guid>
    {
        public virtual User User { get; set; }
        public virtual Company Company { get; set; }
        public string Token { get; set; }
        public DateTime LastLoginOn { get; set; }
        public string Channel { get; set; }
        public string DeviceId { get; set; }

        public int TimeOut { get; set; }
    }
}
