using Fix;
using SRM.Domain.Individuals.InstructorManagement.Services;
using SRM.Domain.Individuals.Parameters.Services;
using SRM.Domain.Individuals.StudentManagement.Services;

namespace SRM.Domain.Individuals.StudentManagement
{
    public interface IStudentDomain : IDependency
    {
        IStudentService StudentService { get; }
        IStudentLessonService StudentLessonService { get; }
        IStudentAddressService StudentAddressService { get; }
        IStudentReportService StudentReportService { get; }
        IStudentAvailableTimeService StudentAvailableTimeService { get; }
        IStudentInstructorRelationService StudentInstructorRelationService { get; }
        IInstructorService InstructorService { get; }
        IHospitalService HospitalService { get; }
        IObstacleTypeService ObstacleTypeService { get; }
        IBranchService BranchService { get; }
        IStudentContactService StudentContactService { get; }
    }
}
