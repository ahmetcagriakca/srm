using FluentValidation;
using SRM.Data.Models.Shuttles.TemplateManagement;
using SRM.Services.Api.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SRM.Services.Api.Shuttles.OperationManagement.Models
{
    //TODO:validator
    //[Validator(typeof(CreateShuttleTemplateRequestValidator))]
    public class CreateShuttleTemplateRequest
    {
        [Required]
        public int LocationRegionId { get; set; }
        [Required]
        public int StudentServiceId { get; set; }
        [Required]
        public int DayOfWeek { get; set; }
        [Required]
        public DateTime Time { get; set; }

        [Required]
        public bool IsActive { get; set; }

        // public List<long> StudentIds { get; set; }
    }

    public class CreateShuttleTemplateRequestValidator : AbstractValidator<CreateShuttleTemplateRequest>
    {
        public CreateShuttleTemplateRequestValidator()
        {
            RuleFor(x => x.LocationRegionId).NotNull().NotEmpty().WithMessage("Bölge Boş Geçilemez");
            RuleFor(x => x.StudentServiceId).NotNull().NotEmpty().WithMessage("Servis Boş Geçilemez");
            RuleFor(x => x.DayOfWeek).NotNull().NotEmpty().WithMessage("Gün Boş Geçilemez");
            RuleFor(x => x.Time).NotNull().NotEmpty().WithMessage("Saat Boş Geçilemez");

        }

    }


    //TODO:validator
    //[Validator(typeof(UpdateShuttleTemplateRequestValidator))]
    public class UpdateShuttleTemplateRequest : CreateShuttleTemplateRequest
    {
        [Required]
        public int Id { get; set; }

    }

    public class UpdateShuttleTemplateRequestValidator : AbstractValidator<UpdateShuttleTemplateRequest>
    {
        public UpdateShuttleTemplateRequestValidator()
        {
            RuleFor(x => x.LocationRegionId).NotNull().NotEmpty().WithMessage("Bölge Boş Geçilemez");
            RuleFor(x => x.StudentServiceId).NotNull().NotEmpty().WithMessage("Servis Boş Geçilemez");
            RuleFor(x => x.DayOfWeek).NotNull().NotEmpty().WithMessage("Gün Boş Geçilemez");
            RuleFor(x => x.Time).NotNull().NotEmpty().WithMessage("Saat Boş Geçilemez");
            RuleFor(x => x.Id).NotNull().NotEmpty().WithMessage("Id Boş Geçilemez");
        }

    }

    //TODO:validator
    //[Validator(typeof(CreateStudentTemplateRequestValidator))]
    public class CreateStudentTemplateRequest
    {
        [Required]
        public int ShuttleTemplateId { get; set; }
        [Required]
        public long StudentId { get; set; }
        [Required]
        public int LessonCount { get; set; }
        [Required]
        public int Order { get; set; }

    }

    public class CreateStudentTemplateRequestValidator : AbstractValidator<CreateStudentTemplateRequest>
    {
        public CreateStudentTemplateRequestValidator()
        {
            RuleFor(x => x.ShuttleTemplateId).NotNull().NotEmpty().WithMessage("Servis Taslağı Boş Geçilemez");
            RuleFor(x => x.StudentId).NotNull().NotEmpty().WithMessage("Öğrenci Boş Geçilemez");
            RuleFor(x => x.LessonCount).NotNull().NotEmpty().WithMessage("Ders Sayısı Boş Geçilemez");
            RuleFor(x => x.Order).NotNull().NotEmpty().WithMessage("Sıra Boş Geçilemez");
        }

    }

    //TODO:validator
    //[Validator(typeof(UpdateStudentTemplateRequestValidator))]

    public class UpdateStudentTemplateRequest
    {
        [Required]
        public long StudentTemplateId { get; set; }
        [Required]
        public int LessonCount { get; set; }
        [Required]
        public int Order { get; set; }

    }
    public class UpdateStudentTemplateRequestValidator : AbstractValidator<UpdateStudentTemplateRequest>
    {
        public UpdateStudentTemplateRequestValidator()
        {
            RuleFor(x => x.StudentTemplateId).NotNull().NotEmpty().WithMessage("Öğrenci Taslak Id Boş Geçilemez");
            RuleFor(x => x.LessonCount).NotNull().NotEmpty().WithMessage("Ders Sayısı Boş Geçilemez");
            RuleFor(x => x.Order).NotNull().NotEmpty().WithMessage("Sıra Boş Geçilemez");
        }

    }

    public static class Shuttle_Template_Controller_Models
    {
        public static ShuttleTemplate CreateShuttleTemplateToModel(this CreateShuttleTemplateRequest request)
        {
            var model = new ShuttleTemplate()
            {
                DayOfWeek = request.DayOfWeek,
                Time = new TimeSpan(request.Time.Hour, request.Time.Minute, 0),
                IsActive = request.IsActive
            };
            return model;
        }

        public static ShuttleTemplate ToModel(this UpdateShuttleTemplateRequest request)//, ShuttleTemplate entity
        {
            var model = new ShuttleTemplate()
            {
                Id = request.Id,
                DayOfWeek = request.DayOfWeek,
                Time = new TimeSpan(request.Time.Hour, request.Time.Minute, 0),
                IsActive = request.IsActive
            };
            return model;
        }

        public static ShuttleStudentTemplate CreateStudentTemplateToModel(this CreateStudentTemplateRequest request)
        {
            var model = new ShuttleStudentTemplate()
            {
                LessonCount = request.LessonCount,
                Order = request.Order
            };
            return model;
        }

        internal static object GetShuttleTemplateHeaderListResponse(this IEnumerable<ShuttleTemplate> shuttleTemplates)
        {
            var entities = (from so in shuttleTemplates.OrderBy(x => x.Time).OrderBy(x => x.DayOfWeek)
                            select new
                            {
                                LocationRegionName = so.LocationRegion.Name,
                                so.DayOfWeek,
                                // so.HourOfDate,
                                Time = new DateTime(so.Time.Ticks),
                                so.IsActive,

                            }).ToList();
            return new BaseServiceResponse()
            {
                ResultValue = entities
            };
        }
        internal static object GetShuttleTemplateListResponse(this IEnumerable<ShuttleTemplate> shuttleTemplates)
        {
            var entities = (from so in shuttleTemplates.OrderBy(x => x.Time).OrderBy(x => x.DayOfWeek)
                            select new
                            {
                                so.Id,
                                LocationRegion = new
                                {
                                    so.LocationRegion.Id,
                                    so.LocationRegion.Name,
                                },
                                so.DayOfWeek,
                                Time = new DateTime(so.Time.Ticks).AddYears(2000),
                                so.IsActive,
                                Students = so.ShuttleStudentTemplates.Select(s => new
                                {
                                    ShuttleOperationId = so.Id,
                                    ShuttleStudentTemplateId = s.Id,
                                    StudentId = s.Student.Id,
                                    Name = s.Student.Name + " " + s.Student.Surname,
                                    s.Order,
                                    s.LessonCount
                                }).OrderBy(en => en.Order),
                                StudentService = new
                                {
                                    so.StudentService.Id,
                                    so.StudentService.Plate,
                                    so.StudentService.MaxCapacity
                                }

                            }).ToList();
            return new BaseServiceResponse()
            {
                ResultValue = entities
            };
        }

        internal static object GetStudentTemplateListResponse(this IEnumerable<ShuttleStudentTemplate> studentTemplates)
        {
            var entities = (from so in studentTemplates.OrderBy(x => x.Order)
                            select new
                            {
                                so.Student.Id,
                                Name = so.Student.Name + " " + so.Student.Surname,
                                so.Order,
                                so.IsActive,
                                LocationRegionName = so.ShuttleTemplate.LocationRegion.Name,
                                so.LessonCount


                            }).ToList();
            return new BaseServiceResponse()
            {
                ResultValue = entities
            };
        }


    }
}