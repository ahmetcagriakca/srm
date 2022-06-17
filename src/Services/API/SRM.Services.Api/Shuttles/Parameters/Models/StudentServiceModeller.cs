using FluentValidation;
//using SRM.Data.Models.Accounts;
using SRM.Data.Models.Shuttles.Parameters;
using SRM.Services.Api.BaseModel;
using System.Collections.Generic;
using System.Linq;

namespace SRM.Services.Api.Shuttles.Parameters.Models
{
    public class SearchStudentServiceRequest
    {
        public int? Id { get; set; }
        public string Plate { get; set; }
        public int? MaxCapacity { get; set; }
        public long? Driver { get; set; }
        public bool? IsActive { get; set; }
    }
    public class SearchStudentServiceResponse
    {
        public int? Id { get; set; }
        public string Plate { get; set; }
        public int? MaxCapacity { get; set; }
        //public User Driver { get; set; }
        public bool IsActive { get; set; }
    }

    public class GetStudentServiceResponse
    {
        public int? Id { get; set; }
        public string Plate { get; set; }
        public int MaxCapacity { get; set; }
        //public User Driver { get; set; }
        public bool IsActive { get; set; }
    }

    //TODO:validator
    //[Validator(typeof(CreateStudentServiceRequestValidator))]
    public class CreateStudentServiceRequest
    {
        public string Plate { get; set; }
        public int MaxCapacity { get; set; }
        public long Driver { get; set; }
        public bool IsActive { get; set; }
    }
    public class CreateStudentServiceRequestValidator : AbstractValidator<CreateStudentServiceRequest>
    {
        public CreateStudentServiceRequestValidator()
        {
            RuleFor(x => x.Plate).NotNull().NotEmpty().WithMessage("Plaka Bilgisi Boş Geçilemez");
            RuleFor(x => x.MaxCapacity).NotNull().NotEmpty().WithMessage("Servis kapasitesi dolu olamlıdır.");
        }
    }

    //TODO:validator
    //[Validator(typeof(UpdatetudentServiceRequestValidator))]
    public class UpdateStudentServiceRequest
    {
        public int Id { get; set; }
        public string Plate { get; set; }
        public int MaxCapacity { get; set; }
        public long Driver { get; set; }
        public bool IsActive { get; set; }
    }
    public class UpdatetudentServiceRequestValidator : AbstractValidator<UpdateStudentServiceRequest>
    {
        public UpdatetudentServiceRequestValidator()
        {
            RuleFor(x => x.Plate).NotNull().NotEmpty().WithMessage("Plaka Bilgisi Boş Geçilemez");
            RuleFor(x => x.MaxCapacity).NotNull().NotEmpty().WithMessage("Servis kapasitesi dolu olamlıdır.");
        }
    }

    public static class StudentServiceModeller
    {
        public static StudentService ToModel(this CreateStudentServiceRequest request)
        {
            var entity = new StudentService
            {
                Plate = request.Plate,
                MaxCapacity = request.MaxCapacity,
                IsActive = request.IsActive,
            };
            return entity;
        }

        public static StudentService ToModel(this UpdateStudentServiceRequest request, StudentService entity)
        {
            entity.Id = request.Id;
            entity.Plate = request.Plate;
            entity.MaxCapacity = request.MaxCapacity;
            entity.IsActive = request.IsActive;
            return entity;
        }

        public static object ToResponse(IEnumerable<StudentService> entities)
        {
            var values = (from entity in entities
                          select new GetStudentServiceResponse
                          {
                              Id = entity.Id,
                              Plate = entity.Plate,
                              MaxCapacity = entity.MaxCapacity,
                              //Driver = (entity.Driver != null ? new User()
                              //{
                              //    Id = entity.DriverId,
                              //    Username = entity.Driver.Username,
                              //    Name = entity.Driver.Name,
                              //    Surname = entity.Driver.Surname
                              //} : null),
                              IsActive = entity.IsActive,
                          }).ToList();
            return new BaseServiceResponse
            {
                ResultValue = values
            };
        }

        public static object ToResponse(StudentService entity)
        {
            var value = new GetStudentServiceResponse
            {
                Id = entity.Id,
                Plate = entity.Plate,
                MaxCapacity = entity.MaxCapacity,
                IsActive = entity.IsActive,
                //Driver = (entity.Driver != null ? new User()
                //{
                //    Id = entity.Driver.Id,
                //    Username = entity.Driver.Username,
                //    Name = entity.Driver.Name,
                //    Surname = entity.Driver.Surname
                //} : null),
            };
            return new BaseServiceResponse
            {
                ResultValue = value
            };
        }
    }
}
