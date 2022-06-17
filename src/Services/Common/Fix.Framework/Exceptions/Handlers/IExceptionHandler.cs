using System;
using System.Threading.Tasks;

namespace Fix.Exceptions.Handlers
{
    public interface IExceptionHandler : IScoped
    {
        void Handle(Exception exception);
        Task HandleAsync(Exception exception);
    }
}
