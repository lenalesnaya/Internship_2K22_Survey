using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ItechArt.Survey.Repositories;

public static class ServiceCollectionsExtension
{
    public static IServiceCollection AddDatabase(
        this IServiceCollection service,
        IConfiguration configuration)
        => service.AddDbContext<SurveyDbContext>(options => 
            options.UseSqlServer(
                configuration.GetConnectionString("SurveyItechArt")));
}