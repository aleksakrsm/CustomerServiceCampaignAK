using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Application.Abstractions.Persistence
{
    public interface IDbContext : IAsyncDisposable
    {
        DbSet<Reward> Rewards { get; }
        DbSet<Campaign> Campaigns { get; }
        //ChangeTracker ChangeTracker { get; set; }// for debugging purposes

        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
