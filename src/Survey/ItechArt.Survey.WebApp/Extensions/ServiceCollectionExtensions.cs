using System.Reflection;
using ItechArt.Repositories;
using ItechArt.Repositories.Abstractions;
using ItechArt.Survey.DomainModel;
using ItechArt.Survey.Foundation.Authentication;
using ItechArt.Survey.Foundation.Authentication.Abstractions;
using ItechArt.Survey.Foundation.UserService;
using ItechArt.Survey.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserStore = ItechArt.Survey.Foundation.Authentication.Stores.UserStore;
using RoleStore = ItechArt.Survey.Foundation.Authentication.Stores.RoleStore;

namespace ItechArt.Survey.WebApp.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuthenticationService(this IServiceCollection services)
                => services.AddScoped<IAuthenticateService, AuthenticateService>()
            .AddScoped<IUserService, UserService>();

    public static IServiceCollection AddServicesMapper(this IServiceCollection services)
        => services.AddAutoMapper(Assembly.GetExecutingAssembly());

    public static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<SurveyDbContext>(options
            => options.UseSqlServer(configuration.GetConnectionString("SurveyItechArt")));
        services.AddScoped<IUnitOfWork, UnitOfWork<SurveyDbContext>>();

        return services;
    }

    public static IServiceCollection AddIdentity(
        this IServiceCollection service)
        => service
            .AddIdentity<User, Role>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = true;
            })
            .AddRoles<Role>()
            .AddUserStore<UserStore>()
            .AddRoleStore<RoleStore>()
            .AddEntityFrameworkStores<SurveyDbContext>()
            .Services;
}