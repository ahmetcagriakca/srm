using Microsoft.EntityFrameworkCore;

namespace IdentityServer.UnitTest.Facades
{
    public interface IDataInitializer
    {
        void InitializeData(DbContext dbContext);
    }
}