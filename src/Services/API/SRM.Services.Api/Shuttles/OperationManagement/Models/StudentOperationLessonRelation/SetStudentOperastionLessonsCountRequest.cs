using FluentValidation;

namespace SRM.Services.Api.Shuttles.OperationManagement.Models.StudentOperationLessonRelation
{
    //TODO:validator
    //[Validator(typeof(SetStudentOperastionLessonsCountRequestValidator))]
    public class SetStudentOperastionLessonsCountRequest
    {
        public long ShuttleStudentOperationId { get; set; }
        public int ComplatedLessonCount { get; set; }
    }

    public class SetStudentOperastionLessonsCountRequestValidator : AbstractValidator<SetStudentOperastionLessonsCountRequest>
    {
        public SetStudentOperastionLessonsCountRequestValidator()
        {
            RuleFor(x => x.ShuttleStudentOperationId).NotNull().NotEmpty().WithMessage("Öğrenci Operasyon Bilgisi Boş Geçilemez!");
            // RuleFor(x => x.ComplatedLessonCount).NotNull().NotEmpty().WithMessage("Tamamlanan ders sayısı boş geçilemez!");
        }

    }
}