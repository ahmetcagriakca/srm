using Fix.Data;
using Microsoft.EntityFrameworkCore;
using SRM.Data.Models.Shuttles.Parameters;
using SRM.Domain.Parameters.Services;
using SRM.Domain.Shuttles.OperationManagement.Services;
using System.Collections.Generic;
using System.Linq;

namespace SRM.Domain.Shuttles.Parameters.Services
{
    public class LocationRegionService : ParameterService<LocationRegion>, ILocationRegionService
    {
        private readonly IRepository<LocationRegionRelation> locationRegionRelationRepository;
        private readonly IShuttleService shuttleService;
        private readonly IShuttleTemplateService shuttleTemplateService;
        public LocationRegionService(
            IRepository<LocationRegion> repository,
            IRepository<LocationRegionRelation> locationRegionRelationRepository,
            IShuttleService _shuttleService, IShuttleTemplateService _shuttleTemplateService
            ) : base(repository)
        {
            this.locationRegionRelationRepository = locationRegionRelationRepository;
            shuttleService = _shuttleService;
            shuttleTemplateService = _shuttleTemplateService;
        }

        public override LocationRegion GetById(int id)
        {
            return _repository
                    .Table
                    .Include(en => en.RegionRelations)
                    .ThenInclude(en => en.SubRegion)
                    .FirstOrDefault(en => en.Id == id);
        }

        public override IQueryable<LocationRegion> Get()
        {
            return _repository
                    .Table
                    .Include(en => en.RegionRelations)
                    .ThenInclude(en => en.SubRegion)
                    .OrderBy(en => en.Id);
        }

        public IQueryable<LocationRegion> GetActiveRegions()
        {
            return _repository
                    .Table
                    .Include(en => en.RegionRelations)
                    .ThenInclude(en => en.SubRegion)
                    .Where(x => x.IsActive)
                    .OrderBy(en => en.Id);
        }

        public void Create(LocationRegion entity, IEnumerable<int> relations)
        {
            _repository.Add(entity);
            relations.ToList().ForEach(en =>
            {
                locationRegionRelationRepository.Add(new LocationRegionRelation() { MainRegion = entity, SubRegion = GetById(en) });
            });
        }

        public void Update(LocationRegion entity, IEnumerable<int> relations)
        {

            if (entity.RegionRelations.Count() > 0)
            {
                //var enumerator = student.ObstacleTypes.GetEnumerator();
                relations = relations ?? new List<int>();
                var list = entity.RegionRelations.Where(en => !(relations.Contains(en.SubRegion.Id)));
                var length = list.Count();
                for (int i = 0; i < length; i++)
                {
                    var item = list.First();

                    //Servis taslaklarında ilişkiye ait öğrenci varsa silinemez!
                    shuttleTemplateService.CheckDeleteSubRegion(entity.Id, item.SubRegion.Id);

                    //ilişkiden kaldırılan bölge icin üretilmiş önerileri siliyoruz
                    shuttleService.DeleteSubRegionAdvice(entity.Id, item.SubRegion.Id);

                    entity.RegionRelations.Remove(item);
                    relations?.ToList().Remove(item.SubRegion.Id);

                }


            }
            relations?.ToList().ForEach(en =>
            {
                if (!entity.RegionRelations.Any(eno => eno.SubRegion.Id == en) && en != entity.Id)
                {
                    var obstacleType = GetById(en);
                    locationRegionRelationRepository.Add(new LocationRegionRelation() { MainRegion = entity, SubRegion = GetById(en) });
                }
            });
        }

        public LocationRegion GetByCode(int code)
        {
            return _repository
                    .Table
                    .Include(en => en.RegionRelations)
                    .ThenInclude(en => en.SubRegion)
                    .FirstOrDefault(en => en.Code == code);
        }
    }
}
