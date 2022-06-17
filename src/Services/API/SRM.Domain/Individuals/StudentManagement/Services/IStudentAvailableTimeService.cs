using Fix;
using SRM.Data.Models.Individuals.StudentManagement;
using System.Collections.Generic;

namespace SRM.Domain.Individuals.StudentManagement.Services
{
    public interface IStudentAvailableTimeService : IDependency
    {
        #region StudentAvailableTimes
        IEnumerable<StudentAvailableTime> Get(long studentId);

        StudentAvailableTime GetById(long studentId, long id);

        void Create(StudentAvailableTime entity);

        void Update(StudentAvailableTime entity);

        void Delete(long studentId, long id);

        #endregion
    }
}
