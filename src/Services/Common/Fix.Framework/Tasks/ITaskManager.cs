using System.Threading;
using System.Threading.Tasks;

namespace Fix.Tasks
{
    public interface ITaskManager : IDependency
    {
        Task ExecuteAsync(CancellationToken token);
    }
}
