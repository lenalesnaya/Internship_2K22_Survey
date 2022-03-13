using ItechArt.Common.Validation;
using ItechArt.Survey.DomainModel;
using ItechArt.Survey.Foundation.Authentication.Abstractions;
using ItechArt.Survey.Foundation.Authentication.Configuration;
using ItechArt.Survey.Foundation.Authentication.Validation.Abstractions;
using Microsoft.Extensions.Options;

namespace ItechArt.Survey.Foundation.Authentication.Validation;

public class UserValidator : IUserValidator
{
    private RegistrationOptions _options;

    public UserValidator(IOptions<RegistrationOptions> options)
    {
        _options = options.Value;
    }
    public ValidationResult<UserRegistrationErrors> Validate(User user)
    {
        var validationResult = ValidateUserName(user.UserName, _options);

        if (validationResult.HasError)
        {
            return ValidationResult<UserRegistrationErrors>.CreateResultWithError(validationResult.Error);
        }

        validationResult = ValidateEmail(user.Email, _options);

        if (validationResult.HasError)
        {
            return ValidationResult<UserRegistrationErrors>.CreateResultWithError(validationResult.Error);
        }

        return ValidationResult<UserRegistrationErrors>.CreateResultWithoutError();
    }

    public ValidationResult<UserRegistrationErrors> Validate(User user, string password)
    {
        var validationResult = Validate(user);
    
        if (validationResult.HasError)
        {
            return ValidationResult<UserRegistrationErrors>.CreateResultWithError(validationResult.Error);
        }
    
        validationResult = ValidatePassword(password, _options);
    
        if (validationResult.HasError)
        {
            return ValidationResult<UserRegistrationErrors>.CreateResultWithError(validationResult.Error);
        }
    
        return ValidationResult<UserRegistrationErrors>.CreateResultWithoutError();
    }


    private ValidationResult<UserRegistrationErrors> ValidateUserName(string userName, RegistrationOptions options)
    {
        if (string.IsNullOrEmpty(userName))
        {
            return ValidationResult<UserRegistrationErrors>.CreateResultWithError(UserRegistrationErrors.UserNameIsRequired);
        }

        if (!(options.UserNameMinLength <= userName.Length && userName.Length <= options.UserNameMaxLength))
        {
            return ValidationResult<UserRegistrationErrors>.CreateResultWithError(UserRegistrationErrors.InvalidUserNameLength);
        }

        var match = options.UserNamePattern.Match(userName);

        if (string.IsNullOrEmpty(match.Value))
        {
            return ValidationResult<UserRegistrationErrors>.CreateResultWithError(UserRegistrationErrors.IncorrectUserName);
        }

        return ValidationResult<UserRegistrationErrors>.CreateResultWithoutError();
    }

    private ValidationResult<UserRegistrationErrors> ValidateEmail(string email, RegistrationOptions options)
    {
        if (string.IsNullOrEmpty(email))
        {
            return ValidationResult<UserRegistrationErrors>.CreateResultWithError(UserRegistrationErrors.EmailIsRequired);
        }

        var match = options.EmailPattern.Match(email);

        if (string.IsNullOrEmpty(match.Value))
        {
            return ValidationResult<UserRegistrationErrors>.CreateResultWithError(UserRegistrationErrors.IncorrectEmail);
        }

        return ValidationResult<UserRegistrationErrors>.CreateResultWithoutError();
    }

    private ValidationResult<UserRegistrationErrors> ValidatePassword(string password, RegistrationOptions options)
    {
        if (string.IsNullOrEmpty(password))
        {
            return ValidationResult<UserRegistrationErrors>.CreateResultWithError(UserRegistrationErrors.PasswordIsRequired);
        }

        if (!(options.PasswordMinLength <= password.Length && password.Length <= options.PasswordMaxLength))
        {
            return ValidationResult<UserRegistrationErrors>.CreateResultWithError(UserRegistrationErrors.InvalidPasswordLength);
        }

        var match = options.PasswordPattern.Match(password);

        if (string.IsNullOrEmpty(match.Value))
        {
            return ValidationResult<UserRegistrationErrors>.CreateResultWithError(UserRegistrationErrors.IncorrectPassword);
        }

        return ValidationResult<UserRegistrationErrors>.CreateResultWithoutError();
    }
}