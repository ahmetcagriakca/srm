namespace IdentityServer.Models
{
    public class IdentityConfig
    {
        public string CertificatePath { get; set; }
        public string CertificatePassword { get; set; }
        public string Authority { get; set; }
        public string Audience { get; set; }
    }
}
