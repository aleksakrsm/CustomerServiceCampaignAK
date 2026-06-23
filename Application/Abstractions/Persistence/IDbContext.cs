using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.Persistence
{
    public interface IDbContext : IAsyncDisposable
    {
        DbSet<Reward> Rewards { get; }
        DbSet<Campaign> Campaigns { get; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
