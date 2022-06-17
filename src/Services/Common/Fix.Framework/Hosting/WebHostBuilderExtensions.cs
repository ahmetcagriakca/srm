using Microsoft.AspNetCore.Hosting;

namespace Fix.Host
{
    public static class WebHostBuilderExtensions
    {
        public static void Execute(this IWebHostBuilder webHostBuilder)
        {
            webHostBuilder.Build();
        }
    }
}
