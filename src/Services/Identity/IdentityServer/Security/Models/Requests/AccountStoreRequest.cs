using System;
using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Security.Models
{
    //STORE
    public class CreateAccountRequest
    {
        [Required(AllowEmptyStrings = false)]
        [MinLength(6)]
        public string UserName { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        [Required]
        public int RoleId { get; set; }
    }
}
