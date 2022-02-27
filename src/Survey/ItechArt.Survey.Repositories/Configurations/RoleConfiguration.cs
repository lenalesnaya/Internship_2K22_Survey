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
            .HasMaxLength(Role.RoleNameMaxLength)
            .IsRequired();
        builder
            .HasIndex(u => u.Name)
            .IsUnique();
        builder
            .Property(u => u.NormalizedName)
            .HasMaxLength(Role.RoleNameMaxLength)
            .IsRequired();
        builder
            .HasIndex(u => u.NormalizedName)
            .IsUnique();
        builder
            .HasData(new Role() { Id = 1, Name = Role.DefaultRoleName, NormalizedName = Role.DefaultRoleNormalizedName });
    }
}