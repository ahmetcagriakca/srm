using System.Linq;

namespace Fix.Security
{
    public interface IAuthenticationRepository : IDependency
    {
        IQueryable<AuthenticationEntity> Table { get; }
        void CreateAuthenticationEntity(AuthenticationEntity entity);
        void UpdateAuthenticationEntity(AuthenticationEntity entity);
        void DeleteAuthenticationIdentity(long userId);
        AuthenticationEntity GetAuthenticationEntity(string token, string refreshToken);
    }
}
