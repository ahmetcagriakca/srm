using Microsoft.Extensions.DependencyInjection;

namespace Fix.Validation
{
    public static class ValidationExtensions
    {
        public static IMvcBuilder UseValidation<T>(this IMvcBuilder mvcBuilder) where T : IValidationProvider
        {
            mvcBuilder.Services.AddTransient(typeof(IValidationProvider), typeof(T));
            return mvcBuilder;
        }
    }
}
