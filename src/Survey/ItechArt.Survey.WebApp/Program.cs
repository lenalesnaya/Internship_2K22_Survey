using System;
using Serilog;
using ItechArt.Survey.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace ItechArt.Survey.WebApp;

public class Program
{
    public static void Main(string[] args)
    {
        Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

        var host = CreateHostBuilder(args).Build();
        MigrateDbContext<SurveyDbContext>(host.Services);
        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
        => Host.CreateDefaultBuilder(args)
            .UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration))
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