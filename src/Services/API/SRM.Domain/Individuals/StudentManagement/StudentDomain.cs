using SRM.Domain.Individuals.InstructorManagement.Services;
using SRM.Domain.Individuals.Parameters.Services;
using SRM.Domain.Individuals.StudentManagement.Services;
using System;

namespace SRM.Domain.Individuals.StudentManagement
{
    public class StudentDomain : IStudentDomain
    {
        public IStudentService StudentService { get; }
        public IStudentLessonService StudentLessonService { get; }
        public IStudentReportService StudentReportService { get; }
        public IStudentAvailableTimeService StudentAvailableTimeService { get; }
        public IStudentInstructorRelationService StudentInstructorRelationService { get; }
        public IInstructorService InstructorService { get; }
        public IHospitalService HospitalService { get; }
        public IObstacleTypeService ObstacleTypeService { get; }
        public IBranchService BranchService { get; }
        public IStudentAddressService StudentAddressService { get; }
        public IStudentContactService StudentContactService { get; }


        public StudentDomain(
            IStudentService studentService,
            IStudentLessonService studentLessonService,
            IStudentReportService studentReportService,
            IStudentAvailableTimeService studentAvailableTimeService,
            IStudentInstructorRelationService StudentInstructorRelationService,
            IInstructorService InstructorService,
            IHospitalService hospitalService,
            IObstacleTypeService obstacleTypeService,
            IBranchService BranchService,
            IStudentAddressService StudentAddressService,
            IStudentContactService StudentContactService

            )
        {
            this.StudentService = studentService ?? throw new ArgumentNullException(nameof(studentService));
            this.StudentLessonService = studentLessonService ?? throw new ArgumentNullException(nameof(studentLessonService));
            this.StudentReportService = studentReportService ?? throw new ArgumentNullException(nameof(studentReportService));
            this.StudentAvailableTimeService = studentAvailableTimeService ?? throw new ArgumentNullException(nameof(studentAvailableTimeService));
            this.StudentInstructorRelationService = StudentInstructorRelationService ?? throw new ArgumentNullException(nameof(StudentInstructorRelationService));
            this.InstructorService = InstructorService ?? throw new ArgumentNullException(nameof(InstructorService));
            this.HospitalService = hospitalService ?? throw new ArgumentNullException(nameof(hospitalService));
            this.ObstacleTypeService = obstacleTypeService ?? throw new ArgumentNullException(nameof(obstacleTypeService));
            this.BranchService = BranchService ?? throw new ArgumentNullException(nameof(BranchService));
            this.StudentAddressService = StudentAddressService ?? throw new ArgumentNullException(nameof(StudentAddressService));
            this.StudentContactService = StudentContactService ?? throw new ArgumentNullException(nameof(StudentContactService));

        }
    }
}
