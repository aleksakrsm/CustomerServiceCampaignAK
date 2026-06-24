using Domain.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Persistance.Interceptors
{
    public class SoftDeleteInterceptor : SaveChangesInterceptor
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

            HandleSoftDeletes(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        public override InterceptionResult<int> SavingChanges(
            DbContextEventData eventData,
            InterceptionResult<int> result)
        {
            if (eventData.Context is null)
            {
                return base.SavingChanges(eventData, result);
            }

            HandleSoftDeletes(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        private static void HandleSoftDeletes(DbContext context)
        {
            // Materialize the collection to avoid "Collection was modified" exception
            var deletedEntities = context
                .ChangeTracker.Entries<Entity>()
                .Where(e => e.State == EntityState.Deleted)
                .ToList();

            foreach (var entry in deletedEntities)
            {
                // Convert hard delete to soft delete
                entry.State = EntityState.Modified;
                entry.Entity.Delete();
            }
        }
    }
}
