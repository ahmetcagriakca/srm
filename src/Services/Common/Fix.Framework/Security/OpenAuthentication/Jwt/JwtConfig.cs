using Fix.Configuration;
using System.Text;

namespace Fix.Security.OpenAuthentication.Jwt
{
    public class JwtConfig : IConfigurationBase
    {
        public string Issuer { get; set; }
        public string SecretKey { get; set; }
        public string Subject { get; set; }
        public string Audience { get; set; }
        public int ExpiryInMinute { get; set; }
        public int FinalExpiration { get; set; }

        public bool IsValid(out string message)
        {
            message = string.Empty;
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(Issuer))
                sb.AppendLine("Issuer can not be empty");

            if (string.IsNullOrEmpty(SecretKey))
                sb.AppendLine("SecretKey can not be empty");

            if (string.IsNullOrEmpty(Subject))
                sb.AppendLine("Subject can not be empty");

            if (string.IsNullOrEmpty(Audience))
                sb.AppendLine("Audience can not be empty");

            if (ExpiryInMinute == 0)
                sb.AppendLine("ExpiryInMinute can not be less then 1");

            if (sb.Length > 0) message = sb.ToString();

            return !(string.IsNullOrEmpty(Issuer) || string.IsNullOrEmpty(SecretKey) || string.IsNullOrEmpty(Subject) || string.IsNullOrEmpty(Audience) || ExpiryInMinute == 0);
        }

        public bool IsValid()
        {
            return !(string.IsNullOrEmpty(Issuer) || string.IsNullOrEmpty(SecretKey) || string.IsNullOrEmpty(Subject) || string.IsNullOrEmpty(Audience) || ExpiryInMinute == 0);
        }
    }
}
