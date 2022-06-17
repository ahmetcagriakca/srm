using Fix.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace SRM.Data
{
    public static class ModelBuilderExtensions
    {
        public static ModelBuilder TraceableEntity<TEntity>(this ModelBuilder builder, Action<EntityTypeBuilder<TEntity>> buildAction) where TEntity : class, ITraceable
        {
            var modelBuilder = builder.Entity<TEntity>(buildAction);

            return modelBuilder.Entity<TEntity>(x =>
            {
                x.Property(p => p.RowVersion).IsRowVersion().IsConcurrencyToken();
            });
        }
    }
}
