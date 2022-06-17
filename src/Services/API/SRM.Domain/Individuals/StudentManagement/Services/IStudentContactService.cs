using Fix;
using SRM.Data.Models.Individuals.StudentManagement;
using System.Collections.Generic;

namespace SRM.Domain.Individuals.StudentManagement.Services
{
    public interface IStudentContactService : IDependency
    {
        void Create(long studentId, StudentContact entity);
        void Delete(long studentId, long id);
        IEnumerable<StudentContact> Get(long studentId);
        StudentContact GetById(long studentId, long id);
        void Update(long studentId, StudentContact entity);

    }
}