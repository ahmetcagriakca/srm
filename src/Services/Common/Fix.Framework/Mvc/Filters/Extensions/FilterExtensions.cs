using Microsoft.Extensions.DependencyInjection;

namespace Fix.Mvc.Filters.Extensions
{
    public static class FilterExtensions
    {
        public static IMvcBuilder AddFilters(this IMvcBuilder mvcBuilder)
        {
            return mvcBuilder.AddMvcOptions(x =>
              {
                  x.Filters.Add(typeof(FixExceptionFilter), int.MinValue);
                  //x.Filters.Add(typeof(FixAuthorization));
                  x.Filters.Add(typeof(ValidationFilter), 3);
                  x.Filters.Add(typeof(TransactionFilter), int.MaxValue);
              });
        }
    }
}
