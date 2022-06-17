using SRM.Data.Models.Shuttles.Parameters;
using SRM.Domain.Parameters.Services;
using System.Collections.Generic;
using System.Linq;

namespace SRM.Domain.Shuttles.Parameters.Services
{
    public interface ILocationRegionService : IParameterService<LocationRegion>
    {
        void Create(LocationRegion entity, IEnumerable<int> relations);
        void Update(LocationRegion entity, IEnumerable<int> relations);
        LocationRegion GetByCode(int code);
        IQueryable<LocationRegion> GetActiveRegions();
    }
}
