using Fix.Data;
using SRM.Data.Models.Individuals.Parameters;
using SRM.Domain.Parameters.Services;

namespace SRM.Domain.Individuals.Parameters.Services
{
    public class HostpitalService : ParameterService<Hospital>, IHospitalService
    {
        public HostpitalService(IRepository<Hospital> repository) : base(repository)
        {
        }
    }
}
