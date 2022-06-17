using System.Collections.Generic;

namespace IdentityServer.Security.Models
{
    /// <summary>
    /// User response detailed list
    /// </summary>
    public class GetUserResponse
    {
        /// <summary>
        /// User Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// User Name
        /// </summary>
        /// <example>runyoufools</example>
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public bool IsActive { get; set; }
        /// <summary>
        /// User roles
        /// </summary>
        public IEnumerable<UserRoleResponse> Roles { get; set; }
        public UserCompanyResponse Company { get; set; }
    }
}
