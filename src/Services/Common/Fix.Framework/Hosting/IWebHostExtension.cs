using Fix.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace Fix.Hosting
{
    public static class IWebHostExtension
    {
        public static async Task<IWebHost> InitFixHostAsync(this IWebHost webHost)
        {
            return await webHost
               .ExecuteTaskAsync();
        }

        public static async Task<IWebHost> ExecuteTaskAsync(this IWebHost webHost)
        {
            var manager = webHost.Services.GetService(typeof(ITaskManager)) as ITaskManager;
            if (manager != null)
                await manager.ExecuteAsync(CancellationToken.None);
            return webHost;
        }
    }
}
