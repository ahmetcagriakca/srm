using Fix.Data;
using IdentityServer.Infrastructor.Configuration;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Infrastructor
{
    public class IdentityDbContextFactory :
        DesignTimeDbContextFactoryBase<IdentityServerDbContext>
    {
        protected override IdentityServerDbContext CreateNewInstance(
            DbContextOptions<IdentityServerDbContext> options)
        {
            return new IdentityServerDbContext(options);
        }
    }

    public class DbContextLocator : IDbContextLocator
    {
        private readonly IdentityServerDbContext _dbContext;
        public DbContextLocator(IdentityServerDbContext dbContext) => _dbContext = dbContext;
        DbContext IDbContextLocator.Current => _dbContext;
    }
}
