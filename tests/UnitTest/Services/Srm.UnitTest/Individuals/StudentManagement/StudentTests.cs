using Fix.Data;
using Fix.Logging;
using Srm.UnitTest.Facades;
using SRM.Domain.Individuals.StudentManagement;
using SRM.Domain.Shuttles.OperationManagement.Services;
using SRM.Domain.Times.Services;
using SRM.Services.Api.Individuals.StudentManagement.Controllers;
using SRM.Services.Api.Individuals.StudentManagement.Models;
using System;
using Xunit;

namespace Srm.UnitTest.Individuals.StudentManagement
{
    public class StudentTests : TestBase
    {
        [Fact]
        public void instructor_post_put_and_delete_user()
        {
            var studentDomain = ContainerManager.Resolve<IStudentDomain>();
            var logManager = ContainerManager.Resolve<ILogManager>();
            var dateCombinationService = ContainerManager.Resolve<IDateCombinationService>();
            var transactionManager = ContainerManager.Resolve<ITransactionManager>();
            var shuttleService = ContainerManager.Resolve<IShuttleService>();
            StudentController studentController = new StudentController(studentDomain, logManager, dateCombinationService, transactionManager, shuttleService);
            var createStudentRequest = new CreateStudentRequest()
            {
                Name = "Ahmet Çağrı",
                Surname = "Akca",
                IdentityNumber = "23232323231",
                IsActive = true,
                DateOfBirth = DateTime.Now.AddYears(-20),
                ParentName = "baba",
                ParentPhoneNumber = "050505050505"
            };
            studentController.CreateStudent(createStudentRequest);
            SaveChanges();
            var student = studentDomain.StudentService.GetStudentByIdentityNumber("23232323231");
            var updateInstructorRequest = new UpdateStudentRequest()
            {
                Id = student.Id,
                Name = "Ahmet Çağrı",
                Surname = "Akca1",
                IdentityNumber = "23232323232",
                IsActive = true,
                DateOfBirth = DateTime.Now.AddYears(-20),
                ParentName = "baba1",
                ParentPhoneNumber = "050505050505"
            };
            studentController.UpdateStudent(student.Id, updateInstructorRequest);
            SaveChanges();
            student = studentDomain.StudentService.GetStudentByIdentityNumber("23232323232");

            Assert.NotNull(student);
            Assert.True(student.Surname == "Akca1");
            // var studentInstructorRelationController = new StudentInstructorRelationController(studentDomain);
            // var createStudentInstructorRelationRequest= new CreateStudentInstructorRelationRequest()
            // {
            //     Student = student,
            //     Instructor = 
            // }

            // studentInstructorRelationController.Post()


        }

    }
}
