using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskTrackerApp.Domain.Entities;

namespace TaskTrackerApp.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Username)
            .IsRequired()
            .HasMaxLength(50)
            .IsUnicode(true); // nvarchar(50)

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false); 

        builder.Property(x => x.PasswordHash)
            .IsRequired()
            .HasMaxLength(255)
            .IsUnicode(false); 

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .IsRequired(false);

        builder.HasMany(x => x.Tasks)
               .WithOne(t => t.User)
               .HasForeignKey(t => t.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
