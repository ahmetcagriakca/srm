using System;
using System.Threading;
using System.Threading.Tasks;

namespace Fix.Exceptions.Policy
{
    public interface IExceptionPolicy : ISingleton
    {
        void Execute(Exception exception);
        Task ExecuteAsync(Exception exception);
        Task ExecuteAsync(Exception exception, CancellationToken cancellationToken);
    }
}
