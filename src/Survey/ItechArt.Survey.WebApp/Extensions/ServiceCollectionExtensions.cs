using ItechArt.Repositories;
using ItechArt.Repositories.Abstractions;
using ItechArt.Survey.Foundation.Counters;
using ItechArt.Survey.Foundation.Counters.Abstractions;
using ItechArt.Survey.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ItechArt.Survey.WebApp.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCounter(this IServiceCollection services)
        => services.AddScoped<ICounterService, DatabaseCounterService>();

    public static IServiceCollection AddUnitOfWork(this IServiceCollection service)
        => service.AddScoped<IUnitOfWork, UnitOfWork<SurveyDbContext>>();

    public static IServiceCollection AddCustomRepository<TEntity, TRepository, TDerivedRepository>(
    this IServiceCollection services)
    where TEntity : class
    where TRepository : class, IRepository<TEntity>
    where TDerivedRepository : class, TRepository
    => services
        .AddScoped<TRepository, TDerivedRepository>();
}