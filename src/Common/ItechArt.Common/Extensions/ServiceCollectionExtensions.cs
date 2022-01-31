using Microsoft.Extensions.DependencyInjection;

namespace ItechArt.Common.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLoggers<T>(this IServiceCollection services)
        => services.AddSingleton<ILogger<T>, Logger<T>>();
}