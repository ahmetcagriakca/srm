using Fix.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;

namespace IdentityServer.Infrastructor
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
            if (typeof(ITraceable).IsAssignableFrom(entity.GetType()))
            {
                var _entity = entity as ITraceable;
                _entity.ModifiedOn = commonData.ModifiedOn();
                _entity.ModifiedBy = commonData.UserId();
            }
        }
        private void OnInserted(object entity)
        {
            if (typeof(ITraceable).IsAssignableFrom(entity.GetType()))
            {
                var _entity = entity as ITraceable;
                _entity.CreatedOn = commonData.CreatedOn();
                _entity.CreatedBy = commonData.UserId();
            }
        }
        private void OnModified(object entity)
        {
            if (typeof(ITraceable).IsAssignableFrom(entity.GetType()))
            {
                var _entity = entity as ITraceable;
                _entity.ModifiedOn = commonData.CreatedOn();
                _entity.ModifiedBy = commonData.UserId();
            }
        }
    }
}
