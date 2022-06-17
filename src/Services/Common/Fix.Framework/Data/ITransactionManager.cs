using System.Threading.Tasks;

namespace Fix.Data
{
    public interface ITransactionManager : IScoped
    {
        void Commit();
        void Rollback();
        Task RollbackAsync();
    }
}
