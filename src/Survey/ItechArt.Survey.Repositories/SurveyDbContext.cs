using ItechArt.Survey.DomainModel;
using ItechArt.Survey.Repositories.Configurations;
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
        modelBuilder.AddUserConfigurations();
        modelBuilder.AddRoleConfigurations();
        modelBuilder.AddUserRoleConfigurations();
    }
}