using ItechArt.Survey.Foundation.Abstractions.Services;
using ItechArt.Survey.Foundation.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ItechArt.Survey.WebApp.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddService(this IServiceCollection services)
            => services
                .AddScoped<ICounterService, CounterService>();
    }
}