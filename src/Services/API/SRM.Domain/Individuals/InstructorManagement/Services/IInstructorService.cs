using Fix;
using SRM.Data.Models.Individuals.InstructorManagement;
using System.Collections.Generic;

namespace SRM.Domain.Individuals.InstructorManagement.Services
{
    public interface IInstructorService : IDependency
    {
        bool IsInstructorExist(string identityNumber);
        Instructor GetInstructorById(long id);
        IEnumerable<Instructor> GetInstructors();
        void CreateInstructor(Instructor instructor);
        void UpdateInstructor(Instructor instructor);
        void DeleteInstructor(long id);
        Instructor GetInstructorByIdentityNumber(string identityNumber);
        IEnumerable<Instructor> Search(long? id, string identityNumber, string name, string surname, int? branch, bool? isActive);

        void CreateInstructorBranches(Instructor instructor, List<int> idList);
        void UpdateInstructorBranches(Instructor instructor, List<int> idList);
    }
}
