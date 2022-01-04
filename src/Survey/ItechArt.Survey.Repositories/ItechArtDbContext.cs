using ItechArt.Survey.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace ItechArt.Survey.Repositories
{
    public class ItechArtDbContext 
        : DbContext
    {
        public DbSet<Counter> Counters { get; set; }


        public ItechArtDbContext(DbContextOptions<ItechArtDbContext> options)
            : base(options)
        {

        }
    }
}
