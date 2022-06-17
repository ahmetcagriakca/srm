namespace Fix.Security.OpenAuthentication.Windows
{
    public interface IAccountServiceBase : IDependency
    {

        bool TryGetUserContext(string username, out IClientContext context);

    }
}
