using Microsoft.EntityFrameworkCore;
using SRM.Data.Configuration;

namespace SRM.Data
{
    public class SRMDbContextFactory :
        DesignTimeDbContextFactoryBase<SrmDbContext>
    {
        protected override SrmDbContext CreateNewInstance(
            DbContextOptions<SrmDbContext> options)
        {
            return new SrmDbContext(options);
        }
    }
}
