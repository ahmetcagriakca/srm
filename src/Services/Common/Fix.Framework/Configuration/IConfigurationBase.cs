namespace Fix.Configuration
{
    public interface IConfigurationBase
    {
        bool IsValid();
        bool IsValid(out string message);
    }
}
