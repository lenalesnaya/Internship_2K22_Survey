using ItechArt.Repositories;
using ItechArt.Repositories.Abstractions;
using ItechArt.Survey.Foundation.Counters;
using ItechArt.Survey.Foundation.Counters.Abstractions;
using ItechArt.Survey.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ItechArt.Survey.WebApp.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCounter(this IServiceCollection services)
        => services.AddScoped<ICounterService, DatabaseCounterService>();

    public static IServiceCollection AddDatabase(
        this IServiceCollection service,
        IConfiguration configuration)
    {
        service.AddDbContext<SurveyDbContext>(options
            => options.UseSqlServer(configuration.GetConnectionString("SurveyItechArt")));
        service.AddScoped<IUnitOfWork, UnitOfWork<SurveyDbContext>>();
        return service;
    }
}