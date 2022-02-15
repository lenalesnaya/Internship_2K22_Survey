using ItechArt.Survey.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace ItechArt.Survey.Repositories;

public class SurveyDbContext : DbContext
{
    public SurveyDbContext(DbContextOptions<SurveyDbContext> options)
        : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().Property(u => u.UserName).HasMaxLength(30);
        modelBuilder.Entity<User>().Property(u => u.Email).IsRequired();
        modelBuilder.Entity<Role>();
        modelBuilder.Entity<UserRole>().HasKey(u=> new { u.RoleId, u.UserId });
    }
}