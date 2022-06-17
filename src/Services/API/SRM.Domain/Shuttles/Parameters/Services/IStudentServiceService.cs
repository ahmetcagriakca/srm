using SRM.Data.Models.Shuttles.Parameters;
using SRM.Domain.Parameters.Services;

namespace SRM.Domain.Individuals.Parameters.Services
{
    public interface IStudentServiceService : IParameterService<StudentService>
    {
        void Create(StudentService entity, long driver);
        void Update(StudentService entity, long driver);

    }
}
