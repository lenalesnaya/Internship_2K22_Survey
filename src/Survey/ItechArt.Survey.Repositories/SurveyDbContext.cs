using System.Text.Json.Serialization.Metadata;
using ItechArt.Survey.DomainModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
        modelBuilder.Entity<User>();
        modelBuilder.Entity<Role>();
        modelBuilder.Entity<UserRole>().HasKey(u=> new { u.RoleId, u.UserId });
    }
}