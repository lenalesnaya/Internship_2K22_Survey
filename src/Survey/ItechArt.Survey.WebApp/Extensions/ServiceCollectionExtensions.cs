using ItechArt.Survey.Foundation.Counters;
using ItechArt.Survey.Foundation.Counters.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace ItechArt.Survey.WebApp.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCounter(this IServiceCollection services)
        => services
            .AddSingleton<ICounterService, InMemoryCounterService>();
}