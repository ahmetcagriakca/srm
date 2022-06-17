

namespace Fix.Logging
{
    public interface ILogManager : IScoped
    {
        ILogger GetLogger<T>() where T : class, ILogger;
        IChainLogger Logger { get; }
    }
}
