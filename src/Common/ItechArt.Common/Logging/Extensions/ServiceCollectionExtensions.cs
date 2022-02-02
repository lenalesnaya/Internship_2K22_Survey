using ItechArt.Common.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace ItechArt.Common.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLogger(this IServiceCollection services)
        => services.AddSingleton<ILogger, Logger>();
}