namespace IdentityServer.Security.Models
{
    public class GetAccountResponse
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public UserCompanyResponse Company { get; set; }
    }
}
