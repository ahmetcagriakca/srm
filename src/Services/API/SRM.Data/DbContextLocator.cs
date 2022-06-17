using Fix.Data;
using Microsoft.EntityFrameworkCore;

namespace SRM.Data
{
    public class DbContextLocator : IDbContextLocator
    {
        private readonly SrmDbContext _dbContext;
        public DbContextLocator(SrmDbContext dbContext) => _dbContext = dbContext;
        DbContext IDbContextLocator.Current => _dbContext;
    }
}
