using ItechArt.Survey.DomainModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ItechArt.Survey.Repositories.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder
            .Property(u => u.Name)
            .HasMaxLength(Role.NameMaxLength)
            .IsRequired();
        builder
            .HasIndex(u => u.Name)
            .IsUnique();

        builder
            .Property(u => u.NormalizedName)
            .HasMaxLength(Role.NameMaxLength)
            .IsRequired();
        builder
            .HasIndex(u => u.NormalizedName)
            .IsUnique();

        builder
            .HasData(new Role
            {
                Id = 1,
                Name = Role.User,
                NormalizedName = Role.User.ToUpper(),
                ConcurrencyStamp = "7612cd22-c0f0-4801-a3e5-ff7cd1a41302"

            });

        builder
            .HasData(new Role
            {
                Id = 2,
                Name = Role.Admin,
                NormalizedName = Role.Admin.ToUpper(),
                ConcurrencyStamp = "7612cd22-c0f0-4801-a3e5-ff7cd1a41301"
            });
    }
}