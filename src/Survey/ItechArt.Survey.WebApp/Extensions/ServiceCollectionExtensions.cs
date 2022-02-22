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
using ItechArt.Survey.Foundation.Authentication.Configuration;
using System.Text.RegularExpressions;

namespace ItechArt.Survey.WebApp.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuthenticationService(this IServiceCollection services)
        => services
        .AddScoped<IAuthenticateService, AuthenticateService>()
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

    public static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services
            .AddIdentity<User, Role>(options =>
            {
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
            .AddRoles<Role>()
            .AddUserStore<UserStore>()
            .AddRoleStore<RoleStore>()
            .AddEntityFrameworkStores<SurveyDbContext>();

        return services;
    }


    public static IServiceCollection AddAuthenticationConfiguration(this IServiceCollection services) // или переместить это в AddAuthenticationService
        => services
        .Configure<RegistrationOptions>(options =>
        {
            options.UserNameIsRequired = true;
            options.UserNameMinLength = 3;
            options.UserNameMaxLength = 30;
            options.UserNamePattern = new Regex(@"^(?=.{3,30}$)(?![_.0-9])[a-zA-ZА-Яа-я0-9._]+(?<![_.])$");
            options.EmailIsRequired = true;
            options.EmailPattern = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                             @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                             @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            options.PasswordIsRequired = true;
            options.PasswordMinLength = 8;
            options.PasswordMaxLength = 20;
            options.PasswordPattern = new Regex(@"^(?=.*[a-z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,20}$");
        });
}