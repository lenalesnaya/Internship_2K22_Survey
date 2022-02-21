using ItechArt.Survey.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace ItechArt.Survey.Repositories;

public class SurveyDbContext : DbContext
{
    private const int MAX_LENGTH = 30;


    public SurveyDbContext(DbContextOptions<SurveyDbContext> options)
        : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .Property(u => u.UserName)
            .HasMaxLength(MAX_LENGTH);
        modelBuilder.Entity<User>()
            .Property(u => u.Email)
            .IsRequired();
        modelBuilder.Entity<User>()
            .Property(u => u.PasswordHash)
            .IsRequired();
        modelBuilder.Entity<Role>()
            .Property(u => u.Name)
            .IsRequired();
        modelBuilder.Entity<UserRole>().HasKey(u => new { u.RoleId, u.UserId });
        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(u => u.RoleId);
        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(u => u.UserId);
    }
}