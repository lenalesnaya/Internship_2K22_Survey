using ItechArt.Survey.DomainModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ItechArt.Survey.Repositories.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasKey(u => new { u.RoleId, u.UserId });
        builder
            .HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(u => u.RoleId)
            .IsRequired();
        builder
            .HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(u => u.UserId)
            .IsRequired();

        builder.HasData(new UserRole
        {
            RoleId = 1,
            UserId = -1
        });

        builder.HasData(new UserRole
        {
            RoleId = 2,
            UserId = -1
        });
    }
}