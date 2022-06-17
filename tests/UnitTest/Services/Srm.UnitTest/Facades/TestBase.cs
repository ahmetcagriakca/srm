using Fix.Data;

namespace Srm.UnitTest.Facades
{
    public class TestBase
    {
        public TestBase(bool UseInMemory = true)
        {
            /// this will be call to cunstructor

            BuildContainer(UseInMemory);
        }

        public void BuildContainer(bool UseInMemory, long userId = 1, long corporationId = 1)
        {
            if (ContainerManager == null)
            {
                ContainerManager = new TestContainerManager(UseInMemory);
            }
            else
            {
                ContainerManager.BuildContainer(UseInMemory, userId, corporationId);

            }
            //if (DataManager == null)
            //{
            //    DataManager = new TestDataManager(ContainerManager);
            //}
            //else
            //{
            //    DataManager.CreateBaseData(ContainerManager);

            //}
        }

        protected TestContainerManager ContainerManager;
        //protected TestDataManager DataManager;

        public void SaveChanges()
        {
            var transactionManager = ContainerManager.Resolve<ITransactionManager>();
            transactionManager.Commit();
        }
    }
}
