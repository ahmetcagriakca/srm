using FluentValidation;
using SRM.Data.Models.Individuals.StudentManagement;
using SRM.Services.Api.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SRM.Services.Api.Individuals.StudentManagement.Models
{

    public class SearchStudentRequest
    {
        public long? Id { get; set; }

        [MaxLength(11)]
        public string IdentityNumber
        {
            get;
            set;
        }
        [MaxLength(50)]
        public string Name
        {
            get;
            set;
        }
        [MaxLength(50)]
        public string Surname
        {
            get;
            set;
        }
        [MaxLength(50)]
        public int? ObstacleType { get; set; }
        public DateTime? ReportStartDate { get; set; }
        public DateTime? ReportEndDate { get; set; }
        public bool? IsActive { get; set; }
        public int? LocationRegion { get; set; }

    }

    public class SearchStudentResponse
    {
        public long? Id { get; set; }


        [Required(AllowEmptyStrings = false)]
        [MaxLength(11)]
        [MinLength(11)]
        public string IdentityNumber
        {
            get;
            set;
        }
        [MaxLength(50)]
        public string Name
        {
            get;
            set;
        }
        [MaxLength(50)]
        public string Surname
        {
            get;
            set;
        }
        [MaxLength(50)]
        public string ObstacleType { get; set; }
        public DateTime? ReportStartDate { get; set; }
        public DateTime? ReportEndDate { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public int? LocationRegion { get; set; }
    }

    //TODO:validator
    //[Validator(typeof(CreateStudentRequestValidator))]
    public class CreateStudentRequest
    {
        [Required(AllowEmptyStrings = false)]
        // [MaxLength(11)]
        // [MinLength(11)]
        public string IdentityNumber
        {
            get;
            set;
        }
        [MaxLength(50)]
        public string Name
        {
            get;
            set;
        }
        [MaxLength(50)]
        public string Surname
        {
            get;
            set;
        }

        public DateTime DateOfBirth { get; set; }

        public string ParentName { get; set; }

        public string ParentPhoneNumber { get; set; }

        public List<int> ObstacleTypes { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
    public class CreateStudentRequestValidator : AbstractValidator<CreateStudentRequest>
    {
        public CreateStudentRequestValidator()
        {
            RuleFor(x => x.IdentityNumber).NotNull().NotEmpty().Length(11).WithMessage("Kimlik Numarası 11 hane olmalıdır");
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Ad boş geçilemez");
            RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(50).WithMessage("Ad 50 karakterden fazla olamaz");
            RuleFor(x => x.Surname).NotNull().NotEmpty().WithMessage("Soyad boş geçilez");
            RuleFor(x => x.Surname).NotNull().NotEmpty().MaximumLength(50).WithMessage("Soyad 50 karakterden fazla olamaz");
            RuleFor(x => x.DateOfBirth).NotNull().NotEmpty().WithMessage("Doğum tarihi boş geçişemez");
            RuleFor(x => x.ParentName).NotNull().NotEmpty().WithMessage("Veli adı boş geçilemez");
            RuleFor(x => x.ParentPhoneNumber).NotNull().NotEmpty().WithMessage("Veli telefon numarası boş geçilemez");

        }
    }

    //TODO:validator
    //[Validator(typeof(UpdateStudentRequestValidator))]

    public class UpdateStudentRequest : CreateStudentRequest
    {
        public long Id { get; set; }

    }
    public class UpdateStudentRequestValidator : AbstractValidator<UpdateStudentRequest>
    {
        public UpdateStudentRequestValidator()
        {
            RuleFor(x => x.IdentityNumber).NotNull().NotEmpty().Length(11).WithMessage("Kimlik Numarası 11 hane olmalıdır");
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Ad boş geçilemez");
            RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(50).WithMessage("Ad 50 karakterden fazla olamaz");
            RuleFor(x => x.Surname).NotNull().NotEmpty().WithMessage("Soyad boş geçilez");
            RuleFor(x => x.Surname).NotNull().NotEmpty().MaximumLength(50).WithMessage("Soyad 50 karakterden fazla olamaz");
            RuleFor(x => x.DateOfBirth).NotNull().NotEmpty().WithMessage("Doğum tarihi boş geçişemez");
            RuleFor(x => x.ParentName).NotNull().NotEmpty().WithMessage("Veli adı boş geçilemez");
            RuleFor(x => x.ParentPhoneNumber).NotNull().NotEmpty().WithMessage("Veli telefon numarası boş geçilemez");

        }
    }

    public class GetReportsResponse
    {
        public long? Id { get; set; }


        [Required(AllowEmptyStrings = false)]
        [MaxLength(11)]
        [MinLength(11)]
        public string IdentityNumber
        {
            get;
            set;
        }
        [MaxLength(50)]
        public string Name
        {
            get;
            set;
        }
        [MaxLength(50)]
        public string Surname
        {
            get;
            set;
        }
        [MaxLength(50)]
        public string ObstacleType { get; set; }
        public DateTime? ReportStartDate { get; set; }
        public DateTime? ReportEndDate { get; set; }
        [Required]
        public bool IsActive { get; set; }

    }
    
    //TODO model değişiklikleri yapılcak
    public static class StudentModeller
    {
        public static Student CreateStudentRequestToModel(this CreateStudentRequest request)
        {
            var model = new Student
            {
                IdentityNumber = request.IdentityNumber,
                Name = request.Name,
                Surname = request.Surname,
                DateOfBirth = request.DateOfBirth,
                IsActive = request.IsActive
            };
            return model;
        }
        public static void UpdateRequestToModel(this UpdateStudentRequest request, ref Student studentModel)
        {
            studentModel.IdentityNumber = request.IdentityNumber;
            studentModel.Name = request.Name;
            studentModel.Surname = request.Surname;
            studentModel.DateOfBirth = request.DateOfBirth;
            studentModel.IsActive = request.IsActive;
        }

        public static object ToSearchStudentsResponse(IEnumerable<Student> students)
        {
            var entities = (from s in students
                            select new
                            {
                                s.Id,
                                s.IdentityNumber,
                                s.Name,
                                s.Surname,
                                ObstacleTypes = s.ObstacleTypes?.Select(en => new { en.ObstacleType.Id, en.ObstacleType.Name,  en.ObstacleType.Description }),
                                ReportStartDate = s.Reports?.LastOrDefault()?.StartDate,
                                ReportEndDate = s.Reports?.LastOrDefault()?.EndDate,
                                isActive = s.IsActive,
                                LocationRegion = new
                                {
                                    s.Addresses.FirstOrDefault()?.Address.LocationRegion?.Id,
                                    s.Addresses.FirstOrDefault()?.Address.LocationRegion?.Name
                                }

                            }).ToList();
            return new BaseServiceResponse
            {
                ResultValue = entities.ToList()
            };
        }

        public static object ToGetStudentsResponse(IEnumerable<Student> students)
        {
            var entities = (from student in students
                            select new
                            {
                                student.Id,
                                student.IdentityNumber,
                                student.Name,
                                student.Surname,
                                student.DateOfBirth,
                                student.ParentName,
                                student.ParentPhoneNumber,
                                ObstacleTypes = student.ObstacleTypes?.Select(en => new { en.ObstacleType.Id, en.ObstacleType.Name, en.ObstacleType.Description }),
                                Address = student.Addresses.FirstOrDefault()?.Address.AddressInfo,
                                student.IsActive,
                                LocationRegion = new
                                {
                                    student.LocationRegion?.Id,
                                    student.LocationRegion?.Name
                                }
                            }).ToList();
            return new BaseServiceResponse
            {
                ResultValue = entities
            };
        }
        public static object ToGetStudentResponse(Student student)
        {
            return new BaseServiceResponse
            {
                ResultValue = new
                {
                    student.Id,
                    student.IdentityNumber,
                    student.Name,
                    student.Surname,
                    student.DateOfBirth,
                    student.ParentName,
                    student.ParentPhoneNumber,
                    ObstacleTypes = student.ObstacleTypes?.Select(en => new { en.ObstacleType.Id, en.ObstacleType.Name, en.ObstacleType.Description }),
                    Address = student.Addresses.FirstOrDefault()?.Address.AddressInfo,
                    student.IsActive,
                    LocationRegion = new
                    {
                        student.LocationRegion?.Id,
                        student.LocationRegion?.Name
                    },
                    Contacts = student.StudentContacts?.Select(x => new
                    {
                        x.Id,
                        x.Name,
                        x.Number
                    })
                }
            };
        }
        public static object ToGetStudentForMobileResponse(Student student)
        {
            return new BaseServiceResponse
            {
                ResultValue = new
                {
                    student.Id,
                    student.IdentityNumber,
                    student.Name,
                    student.Surname,
                    student.DateOfBirth,
                    student.ParentName,
                    student.ParentPhoneNumber,
                    Address = new
                    {
                        student.Addresses.FirstOrDefault()?.Address.Title,
                        AdressInfo = student.Addresses.FirstOrDefault()?.Address.AddressInfo,
                        student.Addresses.FirstOrDefault()?.Address.AddressDirections,
                        Latitude = student.Addresses.FirstOrDefault()?.Address.Location?.LocationX,
                        Longitude = student.Addresses.FirstOrDefault()?.Address.Location?.LocationY,
                        LocationRegionName = student.LocationRegion?.Name
                    },
                    student.IsActive,
                    Contacts = student.StudentContacts?.Select(x => new
                    {
                        x.Id,
                        x.Name,
                        x.Number
                    })

                }
            };
        }
    }
}
