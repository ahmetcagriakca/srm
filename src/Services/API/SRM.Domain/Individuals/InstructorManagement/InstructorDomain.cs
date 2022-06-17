using SRM.Domain.Individuals.InstructorManagement;
using SRM.Domain.Individuals.InstructorManagement.Services;
using System;

namespace SRM.Services.Api.Individuals.InstructorManagement
{
    public class InstructorDomain : IInstructorDomain
    {
        public IInstructorService InstructureService { get; }

        public InstructorDomain(IInstructorService instructureService)
        {
            InstructureService = instructureService ?? throw new ArgumentNullException(nameof(instructureService));
        }
    }
}
