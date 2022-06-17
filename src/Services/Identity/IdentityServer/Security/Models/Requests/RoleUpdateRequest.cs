using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace IdentityServer.Security.Models
{
    public class RoleUpdateRequest
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(255)]
        public string Description { get; set; }
        public bool IsActive { get; set; }
        [MinLength(1)]
        public List<string> Permissions { get; set; }
    }
}
