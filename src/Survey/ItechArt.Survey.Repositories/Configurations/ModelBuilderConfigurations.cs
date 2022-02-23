using ItechArt.Survey.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace ItechArt.Survey.Repositories.Configurations;

public static class ModelBuilderConfigurations
{
    public static ModelBuilder AddUserConfigurations(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .Property(u => u.UserName)
            .IsRequired();
        modelBuilder.Entity<User>()
            .HasIndex(u => u.NormalizedUserName)
            .IsUnique()
            .HasFilter(null);
        modelBuilder.Entity<User>()
            .Property(u => u.Email)
            .IsRequired();
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
        modelBuilder.Entity<User>()
            .HasIndex(u => u.NormalizedEmail)
            .IsUnique()
            .HasFilter(null);
        modelBuilder.Entity<User>()
            .Property(u => u.PasswordHash)
            .IsRequired();

        return modelBuilder;
    }

    public static ModelBuilder AddRoleConfigurations(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>()
            .Property(u => u.Name)
            .IsRequired();
        modelBuilder.Entity<Role>()
            .HasIndex(u => u.Name)
            .IsUnique();
        modelBuilder.Entity<Role>()
            .HasIndex(u => u.NormalizedName)
            .IsUnique()
            .HasFilter(null);

        return modelBuilder;
    }

    public static ModelBuilder AddUserRoleConfigurations(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserRole>().HasKey(u => new { u.RoleId, u.UserId });
        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(u => u.RoleId)
            .IsRequired();
        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(u => u.UserId)
            .IsRequired();
        //modelBuilder.Entity<UserRole>()
        //    .Property(ur => ur.Role)
        //    .HasDefaultValue("User");

        return modelBuilder;
    }
}