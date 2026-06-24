using Domain.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Persistance.Interceptors
{
    public class TimestampAndStateInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
        {
            if (eventData.Context is null)
            {
                return base.SavingChangesAsync(eventData, result, cancellationToken);
            }

            EnforceTrueEntityState(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void EnforceTrueEntityState(DbContext context)
        {
            var entriesEntity = context
            .ChangeTracker.Entries<Entity>()
            .Where(e =>
                e.State == EntityState.Added
                || e.State == EntityState.Modified
                || e.State == EntityState.Deleted)
            .ToList();

            foreach (var entry in entriesEntity)
            {
                // When a new child entity is added to a tracked parent's collection navigation
                // via a backing field (without an explicit DbContext.Add() call), EF Core's snapshot
                // change detection discovers it and tracks it as Modified instead of Added.
                // This occurs on both InMemory and SQL Server because EF Core cannot distinguish
                // a new entity with a client-generated Guid key from a detached existing entity.
                // CreatedAt == DateTime.MinValue is a reliable invariant for a never-saved Entity:
                // the interceptor is the only code that assigns timestamps, and only during save.
                // We assign the timestamps and reset the state to Added so EF Core performs
                // an INSERT rather than an (invalid) UPDATE for a row that does not yet exist.
                var isUnassigned = entry.Entity.CreatedAt == DateTime.MinValue;

                if (entry.State == EntityState.Added || (entry.State == EntityState.Modified && isUnassigned))
                {
                    SetTimestamps(entry);

                    if (entry.State == EntityState.Modified)
                    {
                        entry.State = EntityState.Added;
                    }
                }
                else if (entry.State == EntityState.Modified)
                {
                    UpdateTimestamp(entry);
                }
            }
        }

        private void UpdateTimestamp(EntityEntry<Entity> entry)
        {
            entry.Entity.SetUpdated();
        }

        private void SetTimestamps(EntityEntry<Entity> entry)
        {
            entry.Entity.SetCreated();
        }

        public override InterceptionResult<int> SavingChanges(
            DbContextEventData eventData,
            InterceptionResult<int> result)
        {
            if (eventData.Context is null)
            {
                return base.SavingChanges(eventData, result);
            }

            EnforceTrueEntityState(eventData.Context);
            return base.SavingChanges(eventData, result);
        }
    }
}
