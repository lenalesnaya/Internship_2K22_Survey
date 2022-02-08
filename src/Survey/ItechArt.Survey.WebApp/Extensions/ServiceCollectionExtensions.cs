using System.Reflection;
using ItechArt.Repositories;
using ItechArt.Repositories.Abstractions;
using ItechArt.Survey.DomainModel;
using ItechArt.Survey.Foundation;
using ItechArt.Survey.Foundation.Abstractions;
using ItechArt.Survey.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ItechArt.Survey.WebApp.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuthenticationService(this IServiceCollection services)
        => services.AddScoped<IAuthenticateService, AuthenticateService>();

    public static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<SurveyDbContext>(options
            => options.UseSqlServer(configuration.GetConnectionString("SurveyItechArt")));
        services.AddScoped<IUnitOfWork, UnitOfWork<SurveyDbContext>>();

        return services;
    }

    public static IServiceCollection AddServicesMapper(this IServiceCollection services)
        => services.AddAutoMapper(Assembly.GetExecutingAssembly());

    public static IServiceCollection AddIdentityConfiguration(
        this IServiceCollection service)
        => service
            .AddIdentity()
            .Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            });

    public static IServiceCollection AddIdentity(this IServiceCollection services)
        => services
            .AddIdentity<User, Role>()
            .AddEntityFrameworkStores<SurveyDbContext>()
            .Services;
    //����� AddEntityFrameworkStores() ������������� ��� ���������,
    //������� ����� ����������� � Identity ��� �������� ������.
    //� �������� ���� ��������� ����� ����������� ����� ��������� ������.
}