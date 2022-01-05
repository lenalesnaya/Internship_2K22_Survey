using ItechArt.Survey.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace ItechArt.Survey.Repositories;

public class SurveyDbContext : DbContext
{
    public DbSet<Counter> Counters { get; set; }


    public SurveyDbContext(DbContextOptions<SurveyDbContext> options)
        : base(options)
    {
    }
}
