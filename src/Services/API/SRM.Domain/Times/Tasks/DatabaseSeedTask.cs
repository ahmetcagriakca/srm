using Fix.Data;
using Fix.Security;
using Fix.Tasks;
using SRM.Domain.Times.Services;
using System;
using System.Threading.Tasks;

namespace SRM.Domain.Times.Tasks
{
    public class DatabaseSeedTask : IStartupTask
    {
        private readonly ITransactionManager transactionManager;
        private readonly IDateCombinationService dateCombinationService;
        private readonly IPermissionProvider permissionProvider;

        public DatabaseSeedTask(
            ITransactionManager transactionManager,
            IDateCombinationService dateCombinationService,
            IPermissionProvider permissionProvider)
        {
            this.transactionManager = transactionManager ?? throw new ArgumentNullException(nameof(transactionManager));
            this.dateCombinationService = dateCombinationService;
            this.permissionProvider = permissionProvider ?? throw new ArgumentNullException(nameof(permissionProvider));
        }
        public bool CanStart => true;

        public bool IsAsync => false;

        public void Execute()
        {
            if (!dateCombinationService.HasDateCombination())
            {
                dateCombinationService.CreateDateCombination();
                transactionManager.Commit();
            }
        }

        public Task ExecuteAsync()
        {
            throw new NotImplementedException();
        }
    }
}