using ItechArt.Survey.DomainModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ItechArt.Survey.Repositories;

public class SurveyDbContext : 
    IdentityDbContext<
        User,
    IdentityRole<int>,
    int,
    IdentityUserClaim<int>,
    IdentityUserRole<int>,
    IdentityUserLogin<int>,
    IdentityRoleClaim<int>,
    IdentityUserToken<int>>
{
    public DbSet<Counter> Counters { get; set; }


    public SurveyDbContext(DbContextOptions<SurveyDbContext> options)
        : base(options)
    {
    }
}