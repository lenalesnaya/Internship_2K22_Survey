using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ItechArt.Survey.Repositories;

public class SurveyDbContextFactory : IDesignTimeDbContextFactory<SurveyDbContext>
{
    public SurveyDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SurveyDbContext>();
        optionsBuilder.UseSqlServer("Server=(localdb)\\Server;Database=Database;Trusted_Connection=True");

        return new SurveyDbContext(optionsBuilder.Options);
    }
}