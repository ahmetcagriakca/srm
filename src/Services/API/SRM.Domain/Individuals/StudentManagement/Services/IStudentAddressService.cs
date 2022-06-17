using Fix;
using SRM.Data.Models.Individuals.StudentManagement;
using System.Collections.Generic;

namespace SRM.Domain.Individuals.StudentManagement.Services
{
    public interface IStudentAddressService : IDependency
    {
        void Create(long studentId, StudentAddress entity, int locationRegionId);
        void Delete(long studentId, long id);
        IEnumerable<StudentAddress> Get(long studentId);
        StudentAddress GetById(long studentId, long id);
        void Update(long studentId, StudentAddress entity, int locationRegionId);
    }
}