using Fix.Environment.Dependency;
using Fix.Security;
using System;

namespace Fix
{
    public class WorkContext : IWorkContext
    {

        public WorkContext(
            IDependencySolver dependencySolver,
            IAuthenticationProvider authenticationProvider,
            IAuthenticationRepository authenticationRepository
            )
        {
            DependencySolver = dependencySolver ?? throw new ArgumentNullException(nameof(dependencySolver));
            AuthenticationProvider = authenticationProvider;
            AuthenticationRepository = authenticationRepository;
            CorrelationId = Guid.NewGuid();
        }

        public IDependencySolver DependencySolver { get; }
        public IAuthenticationProvider AuthenticationProvider { get; }
        public IAuthenticationRepository AuthenticationRepository { get; }
        public Guid CorrelationId { get; }
    }
}
