using Srm.UnitTest.Facades;
using SRM.Data.Models.CorporationManagement;
using SRM.Domain.HospitalAppointment.Services;
using SRM.Domain.Individuals.Exceptions.BaseException;
using SRM.Domain.Individuals.InstructorManagement;
using SRM.Services.Api.Individuals.InstructorManagement.Controllers;
using SRM.Services.Api.Individuals.InstructorManagement.Models;
using System;
using Xunit;

namespace Srm.UnitTest.Individuals.InstructorManagement
{
    public class InstructorTests : TestBase
    {
        [Fact]
        public void InstructorPostPutAndDeleteUser()
        {
            var instructorDomain = ContainerManager.Resolve<IInstructorDomain>();
            InstructorController instructorController = new InstructorController(instructorDomain);
            var createInstructorRequest = new CreateInstructorRequest()
            {
                Name = "Ahmet Çağrı",
                Surname = "Akca",
                Email = "test",
                IdentityNumber = "23232323232",
                Phone = "3456789",
                HireDate = DateTime.Now,
                IsActive = true,
            };
            instructorController.Post(createInstructorRequest);
            SaveChanges();
            var instructor = instructorDomain.InstructureService.GetInstructorByIdentityNumber("23232323232");
            var updateInstructorRequest = new UpdateInstructorRequest()
            {
                Id = instructor.Id,
                Name = "Test",
                Surname = "Akca",
                Email = "test",
                IdentityNumber = "23232323232",
                Phone = "3456789",
                HireDate = DateTime.Now,
                IsActive = true,
            };
            instructorController.Put(instructor.Id, updateInstructorRequest);
            SaveChanges();
            instructor = instructorDomain.InstructureService.GetInstructorByIdentityNumber("23232323232");

            Assert.NotNull(instructor);
            //Assert.NotNull(instructor.User);
            //Assert.True(instructor.User.Username == "ahmet.akca");
            //Assert.True(instructor.User.Name == updateInstructorRequest.Name);
            instructorController.Delete(instructor.Id);
            SaveChanges();
            Assert.Throws<RecordNotFoundException>(() => instructorDomain.InstructureService.GetInstructorByIdentityNumber("23232323232"));
        }

        [Fact]
        public void InstructorCorporationTests()
        {
            var instructorDomain = ContainerManager.Resolve<IInstructorDomain>();
            var corporationService = ContainerManager.Resolve<ICorporationService>();
            var corporationHasbahce = new Company()
            {
                Name = "Hasbahce Okulları",
                LongName = "Hasbahce"
            };
            corporationService.Add(corporationHasbahce);
            SaveChanges();
            var corporationCevizAkademi = new Company()
            {
                Name = "Ceviz Akademi Okulları",
                LongName = "Ceviz Akademi"
            };
            corporationService.Add(corporationCevizAkademi);
            SaveChanges();
            {
                InstructorController instructorController = new InstructorController(instructorDomain);
                var createInstructorRequest = new CreateInstructorRequest()
                {
                    Name = "Ahmet Çağrı",
                    Surname = "Akca",
                    Email = "test",
                    IdentityNumber = "23232323232",
                    Phone = "3456789",
                    HireDate = DateTime.Now,
                    IsActive = true,
                };
                instructorController.Post(createInstructorRequest);
                SaveChanges();
            }

            BuildContainer(true, 1, corporationCevizAkademi.Id);

            {
                instructorDomain = ContainerManager.Resolve<IInstructorDomain>();
                InstructorController instructorController = new InstructorController(instructorDomain);
                var createInstructorRequest = new CreateInstructorRequest()
                {
                    Name = "Ahmet Çağrı1",
                    Surname = "Akca",
                    Email = "test1",
                    IdentityNumber = "23232323233",
                    Phone = "3456789",
                    HireDate = DateTime.Now,
                    IsActive = true,
                };
                instructorController.Post(createInstructorRequest);
                SaveChanges();
            }
            Assert.Throws<RecordNotFoundException>(() =>
                instructorDomain.InstructureService.GetInstructorByIdentityNumber("23232323232")
                );
            var instructorCevizAkademi = instructorDomain.InstructureService.GetInstructorByIdentityNumber("23232323233");
            corporationService = ContainerManager.Resolve<ICorporationService>();
            var corporationInstructors = corporationService.GetCorporationInstructor();

            Assert.NotNull(instructorCevizAkademi);
        }
    }
}