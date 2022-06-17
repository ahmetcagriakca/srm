namespace Fix.Configuration
{
    public interface IConfigurationService : IScoped
    {
        T Get<T>(string key);
        T GetSection<T>(string key);
    }
}
