using FluentValidation;

namespace IdentityServer.PageManagement.Models
{
    public class UpdatePageRequestValidator : AbstractValidator<UpdatePageRequest>
    {
        public UpdatePageRequestValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Boş geçilemez");
        }
    }
}
