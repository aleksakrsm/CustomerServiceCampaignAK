using Application.Abstractions.Persistence;
using Domain.Base;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Persistance.Converters;
using System.Linq.Expressions;

namespace Persistance
{
    public class MyDbContext : DbContext, IDbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }
        public DbSet<Reward> Rewards { get; set; } = null!;
        public DbSet<Campaign> Campaigns { get; set; } = null!;

        // for debugging purposes
        //ChangeTracker IDbContext.ChangeTracker { get => ChangeTracker; set => throw new NotImplementedException(); }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<DailyRewardLimit>().HaveConversion<DailyRewardLimitConverter>();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyDbContext).Assembly);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(CreateSoftDeleteFilter(entityType.ClrType));
            }
        }
        private static LambdaExpression CreateSoftDeleteFilter(Type entityType)
        {
            var parameter = Expression.Parameter(entityType, "e");

            // Soft delete filter: IsDeleted == false
            var isDeletedProperty = Expression.Property(parameter, nameof(Entity.IsDeleted));
            var isDeletedFilter = Expression.Equal(isDeletedProperty, Expression.Constant(false));

            return Expression.Lambda(isDeletedFilter, parameter);
        }
    }
}
