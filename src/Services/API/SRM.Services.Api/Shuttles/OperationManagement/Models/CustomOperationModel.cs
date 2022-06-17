using FluentValidation;
using System;

namespace SRM.Services.Api.Shuttles.OperationManagement.Models
{
    //TODO:validator
    //[Validator(typeof(CreateCustomShuttleOperationRequestValidator))]
    public class CreateCustomShuttleOperationRequest
    {
        public DateTime OperationDate { get; set; }
        public int RegionId { get; set; }
        public int StudentServiceId { get; set; }

    }
    public class CreateCustomShuttleOperationRequestValidator : AbstractValidator<CreateCustomShuttleOperationRequest>
    {
        public CreateCustomShuttleOperationRequestValidator()
        {
            RuleFor(x => x.OperationDate).NotNull().NotEmpty().WithMessage("Servis Operasyon tarihi boş geçilemez!");
            RuleFor(x => x.RegionId).NotNull().NotEmpty().WithMessage("Servis Operasyon bölgesi boş geçilemez!");
            RuleFor(x => x.StudentServiceId).NotNull().NotEmpty().WithMessage("Servis Operasyon için Servis Aracı boş geçilemez!");

        }

    }

    //TODO:validator
    //[Validator(typeof(CreateCustomStudentOperationRequestValidator))]
    public class CreateCustomStudentOperationRequest
    {
        public long StudentId { get; set; }
        public long ShuttleOperationId { get; set; }
        public int LessonCount { get; set; }

    }

    public class CreateCustomStudentOperationRequestValidator : AbstractValidator<CreateCustomStudentOperationRequest>
    {
        public CreateCustomStudentOperationRequestValidator()
        {
            RuleFor(x => x.StudentId).NotNull().NotEmpty().WithMessage("Öğrenci bilgisi boş geçilemez!");
            RuleFor(x => x.ShuttleOperationId).NotNull().NotEmpty().WithMessage("Servis Operasyon bilgisi boş geçilemez!");
            RuleFor(x => x.LessonCount).NotNull().NotEmpty().WithMessage("Ders Sayısı boş geçilemez!");

        }

    }



}