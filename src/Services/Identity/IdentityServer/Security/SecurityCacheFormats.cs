namespace IdentityServer.Security
{
    public static class SecurityCacheFormats
    {
        private const string Prefix = "Security";
        public static string UserRoles(int userId)
        {
            return $"{Prefix}_User({userId})";
        }
    }
}
