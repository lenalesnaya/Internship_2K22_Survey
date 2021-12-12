using ItechArt.Survey.Foundation.CounterServices;
using ItechArt.Survey.Foundation.CounterServices.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace ItechArt.Survey.WebApp.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCounter(this IServiceCollection services)
            => services
                .AddScoped<ICounterService, CounterService>();
    }
}