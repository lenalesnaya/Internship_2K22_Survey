using ItechArt.Survey.DomainModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ItechArt.Survey.Repositories
{
    public class ItechArtDbContext 
        : DbContext
    {
        public DbSet<Counter> Counters;

        public ItechArtDbContext(DbContextOptions<ItechArtDbContext> options)
            : base(options)
        {

        }
    }
}
