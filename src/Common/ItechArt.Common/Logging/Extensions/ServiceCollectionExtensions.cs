using Microsoft.Extensions.DependencyInjection;

namespace ItechArt.Common.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLoggers(this IServiceCollection services)
        => services.AddSingleton<ILogger, Logger>();
}