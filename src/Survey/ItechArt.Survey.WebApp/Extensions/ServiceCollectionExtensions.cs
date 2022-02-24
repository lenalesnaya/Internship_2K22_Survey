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
                options.User.AllowedUserNameCharacters = "0123456789_ abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                "àáâãäå¸æçèéêëìíîïğñòóôõö÷øùúûüışÿÀÁÂÃÄÅ¨ÆÇÈÉÊËÌÍÎÏĞÑÒÓÔÕÖ×ØÙÚÛÜİŞß";
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
            .AddRoles<Role>()
            .AddUserStore<UserStore>()
            .AddRoleStore<RoleStore>()
            .AddEntityFrameworkStores<SurveyDbContext>();

        return services;
    }

    public static IServiceCollection AddAuthenticationConfiguration(this IServiceCollection services)
        => services
        .Configure<RegistrationOptions>(options =>
        {
            options.UserNameMinLength = 3;
            options.UserNameMaxLength = 30;
            options.UserNamePattern = new Regex(@"^(?=.{3,30}$)(?![_0-9\s])[a-zA-ZÀ-ßà-ÿ0-9\s_]+(?<![_\s])$");
            options.EmailPattern = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                             @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                             @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            options.PasswordMinLength = 8;
            options.PasswordMaxLength = 20;
            options.PasswordPattern = new Regex(@"^(?=.*[a-z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,20}$");
        });
}