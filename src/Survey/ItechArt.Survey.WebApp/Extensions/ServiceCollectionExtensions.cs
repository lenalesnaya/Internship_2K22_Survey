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
            .AddAuthenticationConfiguration()
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
                options.User.AllowedUserNameCharacters =
                    "0123456789_ abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                    "àáâãäå¸æçèéêëìíîïğñòóôõö÷øùúûüışÿÀÁÂÃÄÅ¨ÆÇÈÉÊËÌÍÎÏĞÑÒÓÔÕÖ×ØÙÚÛÜİŞß";
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
            .AddRoles<Role>()
            .AddUserStore<UserStore>()
            .AddRoleStore<RoleStore>();

        return services;
    }

    public static IServiceCollection AddAuthenticationConfiguration(this IServiceCollection services)
        => services.Configure<RegistrationOptions>(options =>
            {
                options.UserNameMinLength = Constants.RegistrationOptionsConstants.UserNameMinLength;
                options.UserNameMaxLength = Constants.RegistrationOptionsConstants.UserNameMaxLength;
                options.UserNamePattern = new Regex(Constants.RegistrationOptionsConstants.UserNamePattern);
                options.EmailPattern = new Regex(Constants.RegistrationOptionsConstants.EmailPattern);
                options.PasswordMinLength = Constants.RegistrationOptionsConstants.PasswordMinLength;
                options.PasswordMaxLength = Constants.RegistrationOptionsConstants.PasswordMaxLength;
                options.PasswordPattern = new Regex(Constants.RegistrationOptionsConstants.PasswordPattern);
            });
}