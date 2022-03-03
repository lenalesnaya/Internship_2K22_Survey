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
    private readonly RegistrationOptions _options;


    public AuthenticateService(UserManager<User> userManager, IOptions<RegistrationOptions> options)
    {
        _userManager = userManager;
        _options = options.Value;
    }


    public async Task<OperationResult<User, UserRegistrationErrors>> RegisterAsync(User user, string password)
    {
        var validationResult = ValidateUserName(user.UserName);

        if (validationResult.HasError)
        {
            return OperationResult<User, UserRegistrationErrors>.CreateFailureResult(validationResult.Error);
        }

        validationResult = ValidateEmail(user.Email);

        if (validationResult.HasError)
        {
            return OperationResult<User, UserRegistrationErrors>.CreateFailureResult(validationResult.Error);
        }

        validationResult = ValidatePassword(password);

        if (validationResult.HasError)
        {
            return OperationResult<User, UserRegistrationErrors>.CreateFailureResult(validationResult.Error);
        }

        var userExists = await _userManager.FindByNameAsync(user.UserName);

        if (userExists != null)
        {
            return OperationResult<User, UserRegistrationErrors>.CreateFailureResult(UserRegistrationErrors.UserNameAlreadyExists);
        }

        userExists = await _userManager.FindByEmailAsync(user.Email);

        if (userExists != null)
        {
            return OperationResult<User, UserRegistrationErrors>.CreateFailureResult(UserRegistrationErrors.EmailAlreadyExists);
        }

        var createResult = await _userManager.CreateAsync(user, password);

        if (!createResult.Succeeded)
        {
            return OperationResult<User, UserRegistrationErrors>.CreateFailureResult(UserRegistrationErrors.UnknownError);
        }

        var roleResult = await _userManager.AddToRoleAsync(user, Role.User);

        return roleResult.Succeeded
            ? OperationResult<User, UserRegistrationErrors>.CreateSuccessfulResult(user)
            : OperationResult<User, UserRegistrationErrors>.CreateFailureResult(UserRegistrationErrors.UnknownError);
    }


    private ValidationResult<UserRegistrationErrors> ValidateUserName(string userName)
    {
        if (string.IsNullOrEmpty(userName))
        {
            return ValidationResult<UserRegistrationErrors>.CreateResultWithError(UserRegistrationErrors.UserNameIsRequired);
        }

        if (userName.Length < _options.UserNameMinLength || userName.Length > _options.UserNameMaxLength)
        {
            return ValidationResult<UserRegistrationErrors>.CreateResultWithError(UserRegistrationErrors.InvalidUserNameLength);
        }

        var match = _options.UserNamePattern.Match(userName);

        if (string.IsNullOrEmpty(match.Value))
        {
            return ValidationResult<UserRegistrationErrors>.CreateResultWithError(UserRegistrationErrors.IncorrectUserName);
        }

        return ValidationResult<UserRegistrationErrors>.CreateResultWithoutError();
    }

    private ValidationResult<UserRegistrationErrors> ValidateEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return ValidationResult<UserRegistrationErrors>.CreateResultWithError(UserRegistrationErrors.EmailIsRequired);
        }

        var match = _options.EmailPattern.Match(email);

        if (string.IsNullOrEmpty(match.Value))
        {
            return ValidationResult<UserRegistrationErrors>.CreateResultWithError(UserRegistrationErrors.IncorrectEmail);
        }

        return ValidationResult<UserRegistrationErrors>.CreateResultWithoutError();
    }

    private ValidationResult<UserRegistrationErrors> ValidatePassword(string password)
    {
        if (string.IsNullOrEmpty(password))
        {
             return ValidationResult<UserRegistrationErrors>.CreateResultWithError(UserRegistrationErrors.PasswordIsRequired);
        }

        if (password.Length < _options.PasswordMinLength || password.Length > _options.PasswordMaxLength)
        {
            return ValidationResult<UserRegistrationErrors>.CreateResultWithError(UserRegistrationErrors.InvalidPasswordLength);
        }

        var match = _options.PasswordPattern.Match(password);

        if (string.IsNullOrEmpty(match.Value))
        {
            return ValidationResult<UserRegistrationErrors>.CreateResultWithError(UserRegistrationErrors.IncorrectPassword);
        }

        return ValidationResult<UserRegistrationErrors>.CreateResultWithoutError();
    }
}