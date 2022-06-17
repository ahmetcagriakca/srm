using Fix.Configuration;
using System.Text;

namespace Fix.Security.OpenAuthentication.Windows
{
    public class WindowsConfig : IConfigurationBase
    {
        public string Domain { get; set; }
        public int ExpiryInMinute { get; set; }

        public bool IsValid(out string message)
        {
            message = string.Empty;
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(Domain))
                sb.AppendLine("Domain can not be empty");

            if (ExpiryInMinute == 0)
                sb.AppendLine("ExpiryInMinute can not be less then 1");

            if (sb.Length > 0) message = sb.ToString();

            return !(string.IsNullOrEmpty(Domain) || ExpiryInMinute == 0);
        }

        public bool IsValid()
        {
            return !(string.IsNullOrEmpty(Domain) || ExpiryInMinute == 0);
        }
    }
}
