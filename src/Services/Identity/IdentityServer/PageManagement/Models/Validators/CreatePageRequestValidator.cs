using FluentValidation;

namespace IdentityServer.PageManagement.Models
{
    public class CreatePageRequestValidator : AbstractValidator<CreatePageRequest>
    {
        public CreatePageRequestValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Boş geçilemez");
        }
    }
}
