using System;
using System.Threading;
using System.Threading.Tasks;

namespace Fix.Exceptions
{
    public interface IExceptionManager : IScoped
    {
        void Handle(Exception exception);
        Task HandleAsync(Exception exception);
        Task HandleAsync(Exception exception, CancellationToken cancellationToken);
    }
}
