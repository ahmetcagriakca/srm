using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fix.Data
{
    public sealed class TransactionManager : ITransactionManager
    {
        private readonly IDbContextLocator dbContextLocator;
        private readonly ITransanctionEventHandler transanctionEvent;

        public TransactionManager(IDbContextLocator dbContextLocator, ITransanctionEventHandler transanctionEvent)
        {
            this.dbContextLocator = dbContextLocator ?? throw new ArgumentNullException(nameof(dbContextLocator));
            this.transanctionEvent = transanctionEvent ?? throw new ArgumentNullException(nameof(transanctionEvent));
        }

        public DbContext Context
        {
            get
            {
                return dbContextLocator.Current;
            }
        }
        public void Commit()
        {
            transanctionEvent.OnCommitting(GetChangedEntries());
            Context.SaveChanges();
        }

        public void Rollback()
        {
            RejectChanges();
        }
        public async Task RollbackAsync()
        {
            await Task.Run(Rollback);
        }

        public void RejectChanges()
        {
            foreach (var entry in GetChangedEntries())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.CurrentValues.SetValues(entry.OriginalValues);
                        entry.State = EntityState.Unchanged;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;

                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                }
            }
        }

        public IEnumerable<EntityEntry> GetChangedEntries()
        {
            return Context.ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged);
        }
    }
}
