using ItechArt.Common.Logging.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace ItechArt.Common.Logging;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLogger(this IServiceCollection services)
        => services.AddSingleton<ILogger, Logger>();
}