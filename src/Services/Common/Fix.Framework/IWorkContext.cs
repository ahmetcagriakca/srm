using Fix.Environment.Dependency;
using Fix.Security;
using System;

namespace Fix
{
    public interface IWorkContext : IScoped
    {
        Guid CorrelationId { get; }
        IDependencySolver DependencySolver { get; }
        IAuthenticationProvider AuthenticationProvider { get; }
        IAuthenticationRepository AuthenticationRepository { get; }
    }
}
