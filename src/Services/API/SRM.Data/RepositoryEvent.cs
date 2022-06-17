using Fix.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;

namespace SRM.Data
{
    public class RepositoryEvent : ITransanctionEventHandler
    {
        private readonly IValueProvider commonData;

        public RepositoryEvent(IValueProvider commonData)
        {
            this.commonData = commonData ?? throw new ArgumentNullException(nameof(commonData));
        }

        public void OnCommitting(IEnumerable<EntityEntry> entityEntries)
        {
            foreach (var entry in entityEntries)
            {
                switch (entry.State)
                {
                    case EntityState.Deleted: OnDeleted(entry.Entity); break;
                    case EntityState.Modified: OnModified(entry.Entity); break;
                    case EntityState.Added: OnInserted(entry.Entity); break;
                }
            }
        }

        private void OnDeleted(object entity)
        {
            if (entity is ITraceable entityTraceable)
            {
                entityTraceable.ModifiedOn = commonData.ModifiedOn();
                entityTraceable.ModifiedBy = commonData.UserId();
            }
        }
        private void OnInserted(object entity)
        {
            if (entity is ITraceable entityTraceable)
            {
                entityTraceable.CreatedOn = commonData.CreatedOn();
                entityTraceable.CreatedBy = commonData.UserId();
            }

            if (entity is ICorporable entityCorporable)
            {
                entityCorporable.CompanyId = commonData.CompanyId();
            }
        }
        private void OnModified(object entity)
        {
            if (entity is ITraceable entityTraceable)
            {
                entityTraceable.ModifiedOn = commonData.CreatedOn();
                entityTraceable.ModifiedBy = commonData.UserId();
            }
        }
    }
}
