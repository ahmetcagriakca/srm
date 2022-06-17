using System.Threading.Tasks;

namespace Fix.Tasks
{
    public interface IStartupTask : IDependency
    {
        bool CanStart { get; }
        bool IsAsync { get; }

        void Execute();
        Task ExecuteAsync();
    }
}
