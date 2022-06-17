using Microsoft.Extensions.DependencyInjection;

namespace Fix.Validation.Extensions
{
    public static class FluentValidationExtensions
    {
        public static IMvcBuilder UseFluentValidation(this IMvcBuilder mvcBuilder)
        {
            return mvcBuilder.Services.AddMvc(x =>
             {
                 x.ModelValidatorProviders.Clear();
                 x.ModelValidatorProviders.Add(new FixModelValidatorProvider());
             });
        }
    }
}
