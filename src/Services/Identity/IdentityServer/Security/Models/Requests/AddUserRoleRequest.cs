using System.ComponentModel.DataAnnotations;
namespace IdentityServer.Security.Models
{
    public class AddUserRoleRequest
    {
        [Required]
        public int RoleId { get; set; }
    }
}
