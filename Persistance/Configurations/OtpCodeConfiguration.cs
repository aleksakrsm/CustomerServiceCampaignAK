using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Configurations
{
    public class OtpCodeConfiguration : IEntityTypeConfiguration<OtpCode>
    {
        public void Configure(EntityTypeBuilder<OtpCode> builder)
        {
            builder.ToTable("OtpCode");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Email).IsRequired().HasMaxLength(256);
            builder.Property(o => o.Code).IsRequired().HasMaxLength(10);
            builder.Property(o => o.ExpiresAt).IsRequired();
            builder.Property(o => o.IsUsed).IsRequired();
            builder.Property(o => o.UsedAt);
            builder.HasIndex(o => new
            {
                o.Email,
                o.ExpiresAt,
            });
        }
    }
}
