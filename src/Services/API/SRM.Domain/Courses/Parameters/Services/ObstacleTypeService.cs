using Fix.Data;
using SRM.Data.Models.Courses.Parameters;
using SRM.Domain.Individuals.Parameters.Services;
using SRM.Domain.Parameters.Services;

namespace SRM.Domain.Courses.Parameters.Services
{
    public class BranchService : ParameterService<Branch>, IBranchService
    {
        public BranchService(IRepository<Branch> repository) : base(repository)
        {
        }
    }
}
