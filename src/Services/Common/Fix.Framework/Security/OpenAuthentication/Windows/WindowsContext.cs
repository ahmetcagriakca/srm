using System;

namespace Fix.Security.OpenAuthentication.Windows
{
    public class WindowsContext : IIdentityContext
    {
        public string Key { get; set; }
        public string UserName { get; set; }
        public DateTime ExpiredOn { get; set; }

    }
}
