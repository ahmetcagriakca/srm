using Fix.Data;

namespace SRM.Data.Models.CorporationManagement
{
    public class Company : AuthEntity<long>
    {
        public string Name { get; set; }
        public string LongName { get; set; }
    }
}
