using FluentValidation;
namespace IdentityServer.Security.Models
{
    public class UpdateRoleRequestValidator : AbstractValidator<UpdateRoleRequest>
    {
        public UpdateRoleRequestValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Boş geçilemez");
        }
    }
}
