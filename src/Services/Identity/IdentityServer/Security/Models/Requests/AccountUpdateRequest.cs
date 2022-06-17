using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Security.Models
{
    //UPDATE
    public class AccountUpdateRequest
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string Surname { get; set; }
        public bool IsActive { get; set; }
        public IList<int> RoleIds { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
