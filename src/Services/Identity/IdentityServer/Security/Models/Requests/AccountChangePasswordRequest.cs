using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Security.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountChangePasswordRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [MinLength(6)]
        public string OldPassword { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [MinLength(6)]
        public string NewPassword { get; set; }
    }
}
