using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Configurations
{
    public class UserConfiguration : EntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));
            builder.HasKey(o => o.Id);
            builder.Property(u => u.Role).IsRequired().HasConversion<int>();
            builder.Property(c => c.Email)
                .IsRequired()
                .HasConversion(
                            v => v.Value,
                            v => Domain.ValueObjects.Email.Create(v));
            builder.HasIndex(u => u.Email).IsUnique().HasDatabaseName("IX_User_Email");
        }
    }
}
