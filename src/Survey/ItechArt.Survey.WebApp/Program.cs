using System;
using Serilog;
using ItechArt.Survey.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ItechArt.Survey.WebApp;

public class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration().WriteTo.File(
            "bin/Debug/net6.0/logs/Survey.log",
            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();
        Log.Information("Started!");

        try
        {
            var host = CreateHostBuilder(args).Build();
            MigrateDbContext<SurveyDbContext>(host.Services);
            host.Run();

            Log.Information("Stopped cleanly!");
        }
        catch (Exception exception)
        {
            Log.Fatal(exception, exception.Message);
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
        => Host.CreateDefaultBuilder(args)
            .UseSerilog((context, services, configuration)
            => configuration.ReadFrom.Configuration(context.Configuration)
                 .ReadFrom.Services(services)
                 .Enrich.FromLogContext()
                 .WriteTo.File(
                "bin/Debug/net6.0/logs/Survey.log",
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}"))
            .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());


    private static void MigrateDbContext<TContext>(IServiceProvider serviceProvider)
        where TContext : DbContext
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<TContext>();
            context.Database.Migrate();
        }
    }
}