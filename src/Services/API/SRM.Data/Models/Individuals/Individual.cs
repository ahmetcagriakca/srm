using Fix.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace SRM.Data.Models.Individuals
{
    public abstract class Individual : FixEntity<long>, IActivable
    {
        [Column(Order = 2)]
        public string IdentityNumber { get; set; }
        [Column(Order = 3)]
        public string Name { get; set; }
        [Column(Order = 4)]
        public string Surname { get; set; }
        [Column(Order = 5)]
        public string Email { get; set; }
        [Column(Order = 6)]
        public string Phone { get; set; }

        public bool IsActive { get; set; }
    }
}
