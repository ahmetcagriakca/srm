using Fix.Data;
using System;

namespace Fix.Security
{
    public class AuthenticationEntity : FixEntity<long>, IActivable
    {
        public long UserId { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }

        public DateTime ExpiredOn { get; set; }
        public bool IsActive { get; set; }
    }
}
