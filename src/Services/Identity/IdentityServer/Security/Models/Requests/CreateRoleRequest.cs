using System.ComponentModel.DataAnnotations;
namespace IdentityServer.Security.Models
{
    public class CreateRoleRequest
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(255)]
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
