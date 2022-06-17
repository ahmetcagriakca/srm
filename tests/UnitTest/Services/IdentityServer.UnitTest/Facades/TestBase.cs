using Fix.Data;
using Xunit.Abstractions;

namespace IdentityServer.UnitTest.Facades
{
    public class TestBase
    {

        public readonly ITestOutputHelper output;

        protected TestContainerManager ContainerManager;
        protected TestDataManager DataManager;

        public TestBase(ITestOutputHelper output)
        {
            /// this will be call to cunstructor
            this.output = output;
            BuildContainer(true);
        }

        public void BuildContainer(bool UseInMemory, long userId = 1, long corporationId = 1)
        {
            if (ContainerManager == null)
            {
                ContainerManager = new TestContainerManager();
            }
            else
            {
                ContainerManager.BuildContainer(userId, corporationId);

            }
            if (DataManager == null)
            {
                DataManager = new TestDataManager(ContainerManager);
            }
            else
            {
                DataManager.CreateBaseData(ContainerManager);

            }
        }

        public void SaveChanges()
        {
            var transactionManager = ContainerManager.Resolve<ITransactionManager>();
            transactionManager.Commit();
        }
    }
}
