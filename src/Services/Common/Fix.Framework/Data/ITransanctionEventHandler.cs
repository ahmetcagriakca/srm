using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;

namespace Fix.Data
{
    public interface ITransanctionEventHandler : IDependency
    {
        void OnCommitting(IEnumerable<EntityEntry> entityEntries);
    }
}
