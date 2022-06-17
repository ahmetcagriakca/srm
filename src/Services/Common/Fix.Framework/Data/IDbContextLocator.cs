using Microsoft.EntityFrameworkCore;

namespace Fix.Data
{
    public interface IDbContextLocator : IScoped
    {
        DbContext Current { get; }
    }
}
