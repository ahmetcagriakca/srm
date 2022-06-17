using Fix;
using SRM.Data.Models.CorporationManagement;
using SRM.Data.Models.Individuals.InstructorManagement;
using System.Collections.Generic;

namespace SRM.Domain.HospitalAppointment.Services
{
    public interface ICorporationService : IDependency
    {
        IEnumerable<Company> Get();

        void Add(Company entity);

        Company GetByName(string name);

        bool HasCorporation(string name);
        IEnumerable<Instructor> GetCorporationInstructor();
    }
}