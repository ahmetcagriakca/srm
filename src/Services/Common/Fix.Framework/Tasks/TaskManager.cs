using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Fix.Tasks
{
    public class TaskManager : ITaskManager
    {
        private readonly IEnumerable<IStartupTask> startupTasks;

        public TaskManager(IEnumerable<IStartupTask> startupTasks)
        {
            this.startupTasks = startupTasks;
        }
        public Task ExecuteAsync(CancellationToken token)
        {
            return Task.Run(() =>
             {
                 foreach (var task in startupTasks)
                 {
                     if (!token.IsCancellationRequested)
                     {
                         if (task.CanStart)
                         {
                             if (task.IsAsync)
                             {
                                 task.ExecuteAsync();
                             }
                             else
                             {
                                 task.Execute();
                             }
                         }
                     }
                 }
             });
        }
    }
}
