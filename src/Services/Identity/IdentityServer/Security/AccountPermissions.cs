using Fix.Security;
using System.Collections.Generic;

namespace IdentityServer.Security
{

    public class AccountPermissions : IPermissionProvider
    {
        public const string REGISTER_USER = "Account_Register_User";
        public const string CHANGE_PASSWORD = "Account_Change_Password";
        public const string ACTIVATE_USER = "Account_Activate_User";
        public const string VIEW_USERS = "Account_View_Users";
        public const string VIEW_ONLINE_USERS = "Account_View_Online_Users";
        public const string VIEW_USER = "Account_View_User";
        public const string MANAGE_USER = "Account_Manage_User";
        public const string VIEW_PERMISSIONS = "Account_View_Permissions";
        public const string VIEW_ROLES = "Account_View_Roles";
        public const string MANAGE_ROLES = "Account_Manage_Roles";

        IEnumerable<string> IPermissionProvider.GetPermissions()
        {
            return new[] {
                REGISTER_USER,
                CHANGE_PASSWORD,
                ACTIVATE_USER,
                VIEW_USERS,
                VIEW_ONLINE_USERS,
                VIEW_USER,
                MANAGE_USER,
                VIEW_PERMISSIONS,
                VIEW_ROLES,
                MANAGE_ROLES
            };
        }
    }
}
