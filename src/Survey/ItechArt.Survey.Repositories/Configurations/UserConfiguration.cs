using ItechArt.Survey.DomainModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ItechArt.Survey.Repositories.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .Property(u => u.UserName)
                .HasMaxLength(User.UserNameMaxLength)
                .IsRequired();
            builder
                .HasIndex(u => u.UserName)
                .IsUnique();
            builder
                .Property(u => u.NormalizedUserName)
                .HasMaxLength(User.UserNameMaxLength)
                .IsRequired();
            builder
                .HasIndex(u => u.NormalizedUserName)
                .IsUnique();
            builder
                .Property(u => u.Email)
                .IsRequired();
            builder
                .HasIndex(u => u.Email)
                .IsUnique();
            builder
                .Property(u => u.NormalizedEmail)
                .IsRequired();
            builder
                .HasIndex(u => u.NormalizedEmail)
                .IsUnique();
            builder
                .Property(u => u.PasswordHash)
                .IsRequired();
        }
    }
}