using FluentValidation;
namespace IdentityServer.Security.Models
{
    public class CreateRoleRequestValidator : AbstractValidator<CreateRoleRequest>
    {
        public CreateRoleRequestValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Boş geçilemez");
        }
    }
}
