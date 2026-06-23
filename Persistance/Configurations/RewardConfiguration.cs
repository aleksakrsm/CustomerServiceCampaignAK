using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Configurations
{
    public class RewardConfiguration : EntityConfiguration<Reward>
    {
        public override void Configure(EntityTypeBuilder<Reward> builder)
        {
            builder.ToTable(nameof(Reward));
            builder.HasKey(o => o.Id);
            builder.Property(o => o.CampaignId).IsRequired();
            builder.Property(o => o.CustomerId).IsRequired();
            builder.Property(o => o.AgentId).IsRequired();
            builder.Property(o => o.Description).IsRequired().HasMaxLength(200);
            //builder.Property(o => o.IsDeleted).IsRequired();

            builder
                .HasOne(x => x.Campaign)
                .WithMany(x => x.Rewards)
                .HasForeignKey(x => x.CampaignId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
