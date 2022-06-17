using Fix.Data;
using SRM.Data.Models.CorporationManagement;
using SRM.Data.Models.Individuals.InstructorManagement;
using System.Collections.Generic;
using System.Linq;

namespace SRM.Domain.HospitalAppointment.Services
{
    public class CorporationService : ICorporationService
    {

        private readonly IRepository<Company> corporationRepository;
        private readonly IRepository<Instructor> _instructorRepository;

        public CorporationService(
            IRepository<Company> corporationRepository,
            IRepository<Instructor> instructorRepository
        )
        {
            this.corporationRepository = corporationRepository;
            _instructorRepository = instructorRepository;
        }

        public IEnumerable<Company> Get()
        {
            var entities = corporationRepository.Table//.ThenInclude(x => x)            
           ;
            return entities;
        }

        public void Add(Company entity)
        {
            corporationRepository.Add(entity);
        }

        public Company GetByName(string name)
        {
            var entity = corporationRepository.Table.FirstOrDefault(en => en.Name == name);
            return entity;
        }

        public bool HasCorporation(string name)
        {
            return corporationRepository.GetAllWithoutRestriction().Any(en => en.Name == name);
        }

        public IEnumerable<Instructor> GetCorporationInstructor()
        {
            var entities = _instructorRepository.GetAllWithoutRestriction()//.ThenInclude(x => x)            
                ;
            return entities.ToList();
        }
    }
}