using Fix;
using SRM.Domain.Individuals.InstructorManagement.Services;

namespace SRM.Domain.Individuals.InstructorManagement
{
    public interface IInstructorDomain : IDependency
    {
        IInstructorService InstructureService { get; }
    }
}
