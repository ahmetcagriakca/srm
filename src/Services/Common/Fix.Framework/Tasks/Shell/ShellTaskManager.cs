using System;
using System.Collections.Generic;

namespace Fix.Tasks.Shell
{
    public class ShellTaskManager : IShellTaskManager
    {
        private readonly IEnumerable<IShellTask> tasks;

        public ShellTaskManager(IEnumerable<IShellTask> tasks)
        {
            this.tasks = tasks ?? throw new ArgumentNullException(nameof(tasks));
        }
        public void Execute()
        {
            foreach (var task in tasks)
            {
                task.Execute();
            }
        }
    }
}
