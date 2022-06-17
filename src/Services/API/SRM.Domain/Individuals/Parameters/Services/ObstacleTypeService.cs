using Fix.Data;
using SRM.Data.Models.Individuals.Parameters;
using SRM.Domain.Parameters.Services;

namespace SRM.Domain.Individuals.Parameters.Services
{
    public class ObstacleTypeService : ParameterService<ObstacleType>, IObstacleTypeService
    {
        public ObstacleTypeService(IRepository<ObstacleType> repository) : base(repository)
        {
        }
    }
}
