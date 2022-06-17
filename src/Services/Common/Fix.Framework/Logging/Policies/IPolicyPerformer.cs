using System.Threading.Tasks;

namespace Fix.Logging.Policies
{
    public interface IPolicyPerformer : IScoped
    {
        void Perform<T>(BasePolicy policy, LogData<T> logData) where T : class;
        Task PerformAsync<T>(BasePolicy policy, LogData<T> logData) where T : class;
    }
}
