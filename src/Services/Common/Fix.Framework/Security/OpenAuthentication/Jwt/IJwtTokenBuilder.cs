namespace Fix.Security.OpenAuthentication.Jwt
{
    public interface IJwtTokenBuilder : IScoped
    {
        T Create<T>(string key, out string jwtToken) where T : IIdentityContext;
    }
}
