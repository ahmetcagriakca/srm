using Fix.Data;

namespace Srm.UnitTest
{
    public static class Extensions
    {
        public static void Commit(this TestContainerManager containerManager)
        {
            var transactionManager = containerManager.Resolve<ITransactionManager>();
            transactionManager.Commit();
        }
    }
}
