namespace Fix.Logging
{
    public interface ILogDataFactory : IScoped
    {
        LogData<T> Create<T>(LogLevel logLevel, T instance, string message) where T : class;
    }
}
