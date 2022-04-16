using System;
using System.Threading.Tasks;
using ItechArt.Common;
using ItechArt.Common.Logging.Abstractions;
using ItechArt.Common.Logging.Extensions;
using ItechArt.Survey.DomainModel.UserModel;
using ItechArt.Survey.Foundation.Authentication.Abstractions;
using ItechArt.Survey.Foundation.UserManagement.Validation.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace ItechArt.Survey.Foundation.Authentication;

public class AuthenticateService : IAuthenticateService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IUserValidator _userValidator;
    private readonly ILogger _logger;


    public AuthenticateService(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IUserValidator userValidator,
        ILogger logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _userValidator = userValidator;
        _logger = logger;
    }


    public async Task<OperationResult<User, UserRegistrationErrors>> RegisterAsync(User user, string password)
    {
        var validationResult = _userValidator.Validate(user, password);
        if (!validationResult.IsSuccessful)
        {
            _logger.LogWarning($"Validation is failed : {validationResult.Error.GetValueOrDefault()}.");

            return OperationResult<User, UserRegistrationErrors>
                .CreateUnsuccessful(validationResult.Error.GetValueOrDefault());
        }

        var userWithGivenName = await _userManager.FindByNameAsync(user.UserName);
        if (userWithGivenName != null)
        {
            _logger.LogWarning(
                $"The user \"{userWithGivenName}\" is already exists. Username \"{user.UserName}\" can not be used.");

            return OperationResult<User, UserRegistrationErrors>
                .CreateUnsuccessful(UserRegistrationErrors.UserNameAlreadyExists);
        }
        
        var userWithGivenEmail = await _userManager.FindByEmailAsync(user.Email);
        if (userWithGivenEmail != null)
        {
            _logger.LogWarning(
               $"The email \"{userWithGivenEmail}\" is already exists. Email \"{user.Email}\" can not be used.");

            return OperationResult<User, UserRegistrationErrors>
                .CreateUnsuccessful(UserRegistrationErrors.EmailAlreadyExists);
        }

        user.RegistrationDate = DateTime.Now;
        var creationResult = await _userManager.CreateAsync(user, password);
        if (!creationResult.Succeeded)
        {
            _logger.LogWarning(
                $"A creation of the user \"{user.UserName}\" with the email \"{user.Email}\" is failed.");

            foreach (var error in creationResult.Errors)
            {
                _logger.LogWarning(
                    $"{error.Code} - {error.Description}");
            }

            return OperationResult<User, UserRegistrationErrors>
                .CreateUnsuccessful(UserRegistrationErrors.UnknownError);
        }

        _logger.LogInformation(
            $"The user \"{user.UserName}\" with the email \"{user.Email}\" is created successfuly.");

        var addingToRoleResult = await _userManager.AddToRoleAsync(user, Role.User);
        if (!addingToRoleResult.Succeeded)
        {
            _logger.LogWarning(
                $"An adding the user \"{user.UserName}\" with the email \"{user.Email}\" " +
                $"to the role \"{Role.User}\" is failed.");

            foreach (var error in addingToRoleResult.Errors)
            {
                _logger.LogWarning(
                    $"{error.Code} - {error.Description}");
            }

            return OperationResult<User, UserRegistrationErrors>
                .CreateUnsuccessful(UserRegistrationErrors.UnknownError);
        }

        try
        {
            await _signInManager.SignInAsync(user, true);
        }
        catch(Exception exception)
        {
            _logger.LogError(
                $"An authorization of the user \"{user.UserName}\" with the email \"{user.Email}\" is failed.",
                exception);
        }

        return OperationResult<User, UserRegistrationErrors>.CreateSuccessful(user);
    }

    public async Task<OperationResult<UserAuthenticationErrors>> AuthenticateAsync(string userName, string password)
    {
        var signInResult = await _signInManager.PasswordSignInAsync(userName, password, true, false);
        if (!signInResult.Succeeded)
        {
            _logger.LogWarning(
                $"An authorization of the user \"{userName}\" is failed.");

            return OperationResult<UserAuthenticationErrors>
                .CreateUnsuccessful(UserAuthenticationErrors.InvalidUsernameOrPassword);
        }

        return OperationResult<UserAuthenticationErrors>.CreateSuccessful();
    }

    public async Task SignOutAsync()
    {
        try
        {
            await _signInManager.SignOutAsync();
        }
        catch (Exception exception)
        {
            _logger.LogError(
                $"A signing out of the user is failed.",
                exception);
        }
    }
}