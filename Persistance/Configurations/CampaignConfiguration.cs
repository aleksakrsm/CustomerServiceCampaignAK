using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Configurations
{
    public class CampaignConfiguration : EntityConfiguration<Campaign>
    {
        public override void Configure(EntityTypeBuilder<Campaign> builder)
        {
            builder.ToTable(nameof(Campaign));
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(256);
            builder.Property(c => c.StartDate).IsRequired().HasMaxLength(10);
            builder.Property(c => c.EndDate).IsRequired().HasMaxLength(10);
            //builder.Property(c => c.IsDeleted).IsRequired();
            builder.Property(c => c.DailyRewardLimit)
                .IsRequired()
                .HasConversion(
                            v => v.Value,
                            v => DailyRewardLimit.Create(v));

        }
    }
}
