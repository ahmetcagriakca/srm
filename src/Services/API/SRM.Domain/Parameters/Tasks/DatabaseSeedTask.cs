using Fix.Data;
using Fix.Security;
using Fix.Tasks;
using SRM.Data.Models.Parameters;
using SRM.Domain.Parameters.Services;
using System;
using System.Threading.Tasks;

namespace SRM.Domain.Parameters.Tasks
{
    public class DatabaseSeedTask : IStartupTask
    {
        private readonly IApplicationParameterService applicationParameterService;
        private readonly ITransactionManager transactionManager;
        private readonly IPermissionProvider permissionProvider;

        public DatabaseSeedTask(
            IApplicationParameterService applicationParameterService,
            ITransactionManager transactionManager,
            IPermissionProvider permissionProvider)
        {
            this.applicationParameterService = applicationParameterService;
            this.transactionManager = transactionManager ?? throw new ArgumentNullException(nameof(transactionManager));
            this.permissionProvider = permissionProvider ?? throw new ArgumentNullException(nameof(permissionProvider));
        }
        public bool CanStart => true;

        public bool IsAsync => false;

        public void Execute()
        {
            if (!applicationParameterService.HasParameter("DayOfWeek"))
            {
                var applicationParameter = new ApplicationParameter();
                applicationParameter = new ApplicationParameter()
                {
                    Name = "DayOfWeek",
                    Description = "Pazartesi",
                    Value = "1",
                };
                applicationParameterService.Create(applicationParameter);
                applicationParameter = new ApplicationParameter()
                {
                    Name = "DayOfWeek",
                    Description = "Salı",
                    Value = "2",
                };
                applicationParameterService.Create(applicationParameter);
                applicationParameter = new ApplicationParameter()
                {
                    Name = "DayOfWeek",
                    Description = "Çarşamba",
                    Value = "3",
                };
                applicationParameterService.Create(applicationParameter);
                applicationParameter = new ApplicationParameter()
                {
                    Name = "DayOfWeek",
                    Description = "Perşembe",
                    Value = "4",
                };
                applicationParameterService.Create(applicationParameter);
                applicationParameter = new ApplicationParameter()
                {
                    Name = "DayOfWeek",
                    Description = "Cuma",
                    Value = "5",
                };
                applicationParameterService.Create(applicationParameter);
                applicationParameter = new ApplicationParameter()
                {
                    Name = "DayOfWeek",
                    Description = "Cumartesi",
                    Value = "6",
                };
                applicationParameterService.Create(applicationParameter);
                applicationParameter = new ApplicationParameter()
                {
                    Name = "DayOfWeek",
                    Description = "Pazar",
                    Value = "7",
                };
                applicationParameterService.Create(applicationParameter);

                transactionManager.Commit();
            }

            if (!applicationParameterService.HasParameter("ShuttleAdviceCheckDate"))
            {
                var applicationParameter = new ApplicationParameter()
                {
                    Name = "ShuttleAdviceCheckDate",
                    Description = "Servis öneri listesi kaç gün önceye kadara devasızlıklara bakacak.",
                    Value = "90",
                    IsActive = true
                };
                applicationParameterService.Create(applicationParameter);
                transactionManager.Commit();
            }

            if (!applicationParameterService.HasParameter("ShuttleOperationStartTime"))
            {
                var applicationParameter = new ApplicationParameter()
                {
                    Name = "ShuttleOperationStartTime",
                    Description = "Operasyon basla-bitir zaman parametresi",
                    Value = "3",
                    IsActive = true
                };
                applicationParameterService.Create(applicationParameter);
                transactionManager.Commit();
            }


            if (!applicationParameterService.HasParameter("ShuttleTemplateConstraint"))
            {
                var applicationParameter = new ApplicationParameter()
                {
                    Name = "ShuttleTemplateConstraint",
                    Description = "Servis taslaklarında servisin farklı operasyonu başlayabilme icin geçmesi gereken süre",
                    Value = "30",
                    IsActive = true
                };
                applicationParameterService.Create(applicationParameter);
                transactionManager.Commit();
            }

            if (!applicationParameterService.HasParameter("ApplicationUrl"))
            {
                var applicationParameter = new ApplicationParameter()
                {
                    Name = "ApplicationUrl",
                    Description = "Uygulamanın bağlanacağı api adresi",
                    Value = "http://localhost",
                    IsActive = true
                };
                applicationParameterService.Create(applicationParameter);
                transactionManager.Commit();
            }
            if (!applicationParameterService.HasParameter("ApplicationName"))
            {
                var applicationParameter = new ApplicationParameter()
                {
                    Name = "ApplicationName",
                    Description = "Uygulama adı",
                    Value = "Test",
                    IsActive = true
                };
                applicationParameterService.Create(applicationParameter);
                transactionManager.Commit();
            }
        }

        public Task ExecuteAsync()
        {
            throw new NotImplementedException();
        }
    }
}
