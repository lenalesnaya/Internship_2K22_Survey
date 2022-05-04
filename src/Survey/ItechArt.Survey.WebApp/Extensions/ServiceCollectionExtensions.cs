using System.Reflection;
using ItechArt.Repositories;
using ItechArt.Repositories.Abstractions;
using ItechArt.Survey.DomainModel.UserModel;
using ItechArt.Survey.Foundation.Authentication;
using ItechArt.Survey.Foundation.Authentication.Abstractions;
using ItechArt.Survey.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserStore = ItechArt.Survey.Foundation.UserManagement.Stores.UserStore;
using RoleStore = ItechArt.Survey.Foundation.UserManagement.Stores.RoleStore;
using ItechArt.Survey.Foundation.Authentication.Configuration;
using System.Text.RegularExpressions;
using ItechArt.Survey.Foundation.AnswerManagement;
using ItechArt.Survey.Foundation.AnswerManagement.Abstrations;
using ItechArt.Survey.Foundation.QuestionManagement;
using ItechArt.Survey.Foundation.QuestionManagement.Abstractions;
using ItechArt.Survey.Foundation.QuestionManagement.Stores.Abstractions;
using ItechArt.Survey.Foundation.SurveyManagement;
using ItechArt.Survey.Foundation.SurveyManagement.Abstractions;
using ItechArt.Survey.Foundation.SurveyManagement.Stores;
using ItechArt.Survey.Foundation.SurveyManagement.Stores.Abstractions;
using ItechArt.Survey.Foundation.UserManagement.Abstractions;
using ItechArt.Survey.Foundation.UserManagement;
using ItechArt.Survey.Foundation.UserManagement.Validation;
using ItechArt.Survey.Foundation.UserManagement.Validation.Abstractions;
using ItechArt.Survey.Foundation.SurveyManagement.Validation.Abstractions;
using ItechArt.Survey.Foundation.SurveyManagement.Validation;
using ItechArt.Survey.Foundation.UserAnswerManagement;
using ItechArt.Survey.Foundation.UserAnswerManagement.Abstractions;
using ItechArt.Survey.Foundation.UserAnswerManagement.Stores;
using ItechArt.Survey.Foundation.UserAnswerManagement.Stores.Abstractions;
using ItechArt.Survey.WebApp.GoogleDriveManagement;
using ItechArt.Survey.WebApp.GoogleDriveManagement.Abstractions;

namespace ItechArt.Survey.WebApp.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuthenticationService(this IServiceCollection services)
        => services
            .AddScoped<IAuthenticateService, AuthenticateService>()
            .Configure<RegistrationOptions>(options =>
            {
                options.UserNameMinLength = Constants.RegistrationOptionsConstants.UserNameMinLength;
                options.UserNameMaxLength = Constants.RegistrationOptionsConstants.UserNameMaxLength;
                options.UserNamePattern = new Regex(Constants.RegistrationOptionsConstants.UserNamePattern);
                options.EmailPattern = new Regex(Constants.RegistrationOptionsConstants.EmailPattern);
                options.PasswordMinLength = Constants.RegistrationOptionsConstants.PasswordMinLength;
                options.PasswordMaxLength = Constants.RegistrationOptionsConstants.PasswordMaxLength;
                options.PasswordPattern = new Regex(Constants.RegistrationOptionsConstants.PasswordPattern);
            })
            .AddScoped<IUserService, UserService>()
            .AddScoped<IUserValidator, UserValidator>();

    public static IServiceCollection AddStores(this IServiceCollection service)
    {
        service.AddScoped<ISurveyStore, SurveyStore>();
        service.AddScoped<IQuestionStore, QuestionStore>();
        service.AddScoped<IAnswerStore, AnswerStore>();
        service.AddScoped<IUserAnswerStores, UserAnswerStore>();

        return service;
    } 
    
    public static IServiceCollection AddServices(this IServiceCollection service)
    {
        service.AddScoped<ISurveyService, SurveyService>();
        service.AddScoped<IQuestionService, QuestionService>();
        service.AddScoped<ISurveyValidator, SurveyValidator>();
        service.AddScoped<IUserAnswerService, UserAnswerService>();
        service.AddScoped<IGoogleDriveManager, GoogleDriveManager>();
        //service.AddScoped<ISelectedAnswerService, SelectedAnswerService>();

        return service;
    } 

        public static IServiceCollection AddServicesMapper(this IServiceCollection services)
        => services.AddAutoMapper(Assembly.GetExecutingAssembly());

    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SurveyDbContext>(options
            => options
                .UseLazyLoadingProxies()
                .UseSqlServer(configuration.GetConnectionString("SurveyItechArt")));
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
                    "�������������������������������������Ũ��������������������������";
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
            .AddRoles<Role>()
            .AddUserStore<UserStore>()
            .AddRoleStore<RoleStore>();

        return services;
    }
}