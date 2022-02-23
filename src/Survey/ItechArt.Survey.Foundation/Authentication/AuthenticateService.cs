using System.Threading.Tasks;
using ItechArt.Common;
using ItechArt.Survey.DomainModel;
using ItechArt.Survey.Foundation.Authentication.Abstractions;
using ItechArt.Survey.Foundation.Authentication.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace ItechArt.Survey.Foundation.Authentication;

public class AuthenticateService : IAuthenticateService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly RegistrationOptions _options;


    public AuthenticateService(
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        IOptions<RegistrationOptions> options)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _options = options.Value;
    }


    public async Task<OperationResult<User, UserRegistrationErrors>> RegisterAsync(User user, string password)
    {
        var validationResult = Validate(user, password);

        if (validationResult.HasError)
            return OperationResult<User, UserRegistrationErrors>.CreateFailureResult(
                validationResult.Error);

        var userExists = await _userManager.FindByNameAsync(user.UserName);

        if (userExists != null)
        {
            return OperationResult<User, UserRegistrationErrors>.CreateFailureResult(
                UserRegistrationErrors.UserNameAlreadyExists);
        }

        userExists = await _userManager.FindByEmailAsync(user.Email);

        if (userExists != null)
        {
            return OperationResult<User, UserRegistrationErrors>.CreateFailureResult(
                UserRegistrationErrors.EmailAlreadyExists);
        }

        if (await _roleManager.FindByNameAsync("User") == null)
        {
            var userRole = new Role()
            {
                Name = "User"
            };
            await _roleManager.CreateAsync(userRole);
        }

        var createResult = await _userManager.CreateAsync(user, password);
        await _userManager.AddToRoleAsync(user, "User");

        return createResult.Succeeded
            ? OperationResult<User, UserRegistrationErrors>.CreateSuccessfulResult(user)
            : OperationResult<User, UserRegistrationErrors>.CreateFailureResult(UserRegistrationErrors.UnknownError);
    }


    private ValidationResult<UserRegistrationErrors> Validate(User user, string password) 
    {
        if (string.IsNullOrEmpty(user.UserName))
        {
            if (_options.UserNameIsRequired)
            {
                return ValidationResult<UserRegistrationErrors>.CreateResultWithError(UserRegistrationErrors.InvalidUserName);
            }
        }
        else
        {
            if (user.UserName.Length < _options.UserNameMinLength || user.UserName.Length > _options.UserNameMaxLength)
            {
                return ValidationResult<UserRegistrationErrors>.CreateResultWithError(UserRegistrationErrors.InvalidUserName);
            }

            var match = _options.UserNamePattern.Match(user.UserName);

            if (string.IsNullOrEmpty(match.Value))
            {
                return ValidationResult<UserRegistrationErrors>.CreateResultWithError(UserRegistrationErrors.InvalidUserName);
            }
        }

        if (string.IsNullOrEmpty(user.Email))
        {
            if (_options.EmailIsRequired)
            {
                return ValidationResult<UserRegistrationErrors>.CreateResultWithError(UserRegistrationErrors.InvalidEmail);
            }
        }
        else
        {
            var match = _options.EmailPattern.Match(user.Email);

            if (string.IsNullOrEmpty(match.Value))
            {
                return ValidationResult<UserRegistrationErrors>.CreateResultWithError(UserRegistrationErrors.InvalidEmail);
            }
        }

        if (string.IsNullOrEmpty(password))
        {
            if (_options.PasswordIsRequired)
            {
                return ValidationResult<UserRegistrationErrors>.CreateResultWithError(UserRegistrationErrors.InvalidPassword);
            }
        }
        else
        {
            if (password.Length < _options.PasswordMinLength || password.Length > _options.PasswordMaxLength)
            {
                return ValidationResult<UserRegistrationErrors>.CreateResultWithError(UserRegistrationErrors.InvalidPassword);
            }

            var match = _options.PasswordPattern.Match(password);

            if (string.IsNullOrEmpty(match.Value))
            {
                return ValidationResult<UserRegistrationErrors>.CreateResultWithError(UserRegistrationErrors.InvalidPassword);
            }
        }

        return ValidationResult<UserRegistrationErrors>.CreateResultWithoutError();
    }
}