using System;

namespace Fix.Security.OpenAuthentication.Jwt
{
    public class JwtContext : IIdentityContext
    {
        public string Key { get; set; }
        public string RefreshToken { get; set; }
        public string Token { get; set; }
        public string TokenType { get; set; }
        public DateTime ExpiredOn { get; set; }
        public DateTime FinalExpiredOn { get; set; }
        public string Id { get; set; }
    }
}
