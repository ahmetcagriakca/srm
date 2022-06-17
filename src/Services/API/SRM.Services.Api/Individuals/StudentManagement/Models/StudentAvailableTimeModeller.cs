using FluentValidation;
using SRM.Data.Models.Individuals.StudentManagement;
using SRM.Data.Models.Times;
using SRM.Services.Api.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SRM.Services.Api.Individuals.StudentManagement.Models
{
    //TODO:validator
    //[Validator(typeof(CreateStudentAvailableTimeRequestValidator))]
    public class CreateStudentAvailableTimeRequest
    {
        public int StudentId { get; set; }
        //Uygunluk durumu
        public bool IsAvaible { get; set; }

        //Sürekli mi parçalı mı
        public bool IsIntegrated { get; set; }

        /// <summary>
        /// Başlama Tarihi
        /// </summary>
        /// <value>The begin time.</value>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Bitiş Tarihi
        /// </summary>
        /// <value>The end time.</value>
        public DateTime EndDate { get; set; }

        //Entegre olmayan kayıtlar icin başlama saati
        public DateTime? StartTime { get; set; }

        //Entegre olmayan kayıtlar icin bitiş saati
        public DateTime? EndTime { get; set; }

        //İlgili günler kombinasyonu
        public DateCombination IncludedDate { get; set; }

        //Açıklama
        public string Description { get; set; }

    }

    public class CreateStudentAvailableTimeRequestValidator : AbstractValidator<CreateStudentAvailableTimeRequest>
    {
        public CreateStudentAvailableTimeRequestValidator()
        {
            RuleFor(x => x.IsAvaible).NotNull().WithMessage("Müsaitlik durumu boş geçilemez");
            RuleFor(x => x.StartDate).NotNull().WithMessage("Başlama Tarihi boş geçilemez");
            RuleFor(x => x.EndDate).NotNull().WithMessage("Bitiş Tarihi boş geçilemez");
        }
    }

    //TODO:validator
    //[Validator(typeof(UpdateStudentAvailableTimeRequestValidator))]
    public class UpdateStudentAvailableTimeRequest : CreateStudentAvailableTimeRequest
    {
        public long Id { get; set; }

    }
    public class UpdateStudentAvailableTimeRequestValidator : AbstractValidator<UpdateStudentAvailableTimeRequest>
    {
        public UpdateStudentAvailableTimeRequestValidator()
        {
            RuleFor(x => x.IsAvaible).NotNull().WithMessage("Müsaitlik durumu boş geçilemez");
            RuleFor(x => x.StartDate).NotNull().WithMessage("Başlama Tarihi boş geçilemez");
            RuleFor(x => x.EndDate).NotNull().WithMessage("Bitiş Tarihi boş geçilemez");
        }
    }
    public static class StudentAvailableTimeModeller
    {
        public static StudentAvailableTime CreateStudentAvailableTimeRequestToModel(this CreateStudentAvailableTimeRequest request)
        {
            var model = new StudentAvailableTime();
            model.IsAvaible = request.IsAvaible;
            model.IsIntegrated = request.IsIntegrated;
            model.StartDate = request.StartDate;
            model.EndDate = request.EndDate;
            model.StartTime = request.StartTime;
            model.EndTime = request.EndTime;
            model.Description = request.Description;
            return model;
        }
        public static void UpdateStudentAvailableTimeRequestToModel(this UpdateStudentAvailableTimeRequest request, ref StudentAvailableTime model)
        {
            model.IsAvaible = request.IsAvaible;
            model.IsIntegrated = request.IsIntegrated;
            model.StartDate = request.StartDate;
            model.EndDate = request.EndDate;
            model.StartTime = request.StartTime;
            model.EndTime = request.EndTime;
            model.Description = request.Description;
        }
        public static BaseServiceResponse ToGetStudentAvailableTimesResponse(IEnumerable<StudentAvailableTime> entities)
        {
            var responseEntities = from entity in entities
                                   select new
                                   {
                                       entity.Id,
                                       entity.IsAvaible,
                                       entity.IsIntegrated,
                                       entity.StartDate,
                                       entity.EndDate,
                                       entity.StartTime,
                                       entity.EndTime,
                                       entity.IncludedDate,
                                       entity.Description,
                                       Student = new
                                       {
                                           entity.Student.Id,
                                           entity.Student.Name,
                                           entity.Student.Surname,
                                       },
                                   };
            return new BaseServiceResponse
            {
                ResultValue = responseEntities.ToList()
            };
        }

        public static BaseServiceResponse ToGetStudentAvailableTimeResponse(StudentAvailableTime entity)
        {
            var responseEntity = new
            {
                entity.Id,
                entity.IsAvaible,
                entity.IsIntegrated,
                entity.StartDate,
                entity.EndDate,
                entity.StartTime,
                entity.EndTime,
                entity.IncludedDate,
                entity.Description,
                Student = new
                {
                    entity.Student.Id,
                    entity.Student.Name,
                    entity.Student.Surname,
                },
            };
            return new BaseServiceResponse
            {
                ResultValue = responseEntity
            };
        }
    }
}
