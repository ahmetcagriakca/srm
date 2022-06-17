using Autofac;
using Fix.Environment.FeatureManagement;

namespace Fix.Environment.Shell
{
    public interface IWorkScopeBuilder
    {
        void Build(ContainerBuilder builder, FeatureContext context);

    }
}
