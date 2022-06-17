namespace Fix.Security
{
    public interface IAuthorizer : IScoped
    {
        bool Authorize(string permission);
    }
}
