using ItechArt.Survey.DomainModel;
using ItechArt.Survey.Foundation.Authentication.Abstractions;
using ItechArt.Survey.Foundation.Authentication.Configuration;
using ItechArt.Survey.Foundation.Validation.Abstractions;

namespace ItechArt.Survey.Foundation.Validation;

public class UserValidator : IValidator<User, RegistrationOptions, ValidationResult<UserRegistrationErrors>>
{
    public ValidationResult<UserRegistrationErrors> Validate(User user, RegistrationOptions options)
    {
        var validationResult = ValidateUserName(user.UserName, options);

        if (validationResult.HasError)
        {
            return ValidationResult<UserRegistrationErrors>.CreateResultWithError(validationResult.Error);
        }

        validationResult = ValidateEmail(user.Email, options);

        if (validationResult.HasError)
        {
            return ValidationResult<UserRegistrationErrors>.CreateResultWithError(validationResult.Error);
        }

        return ValidationResult<UserRegistrationErrors>.CreateResultWithoutError();
    }

    public ValidationResult<UserRegistrationErrors> Validate(User user, string password, RegistrationOptions options)
    {
        var validationResult = Validate(user, options);

        if (validationResult.HasError)
        {
            return ValidationResult<UserRegistrationErrors>.CreateResultWithError(validationResult.Error);
        }

        validationResult = ValidatePassword(password, options);

        if (validationResult.HasError)
        {
            return ValidationResult<UserRegistrationErrors>.CreateResultWithError(validationResult.Error);
        }

        return ValidationResult<UserRegistrationErrors>.CreateResultWithoutError();
    }


    private static ValidationResult<UserRegistrationErrors> ValidateUserName(string userName, RegistrationOptions options)
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

    private static ValidationResult<UserRegistrationErrors> ValidateEmail(string email, RegistrationOptions options)
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

    private static ValidationResult<UserRegistrationErrors> ValidatePassword(string password, RegistrationOptions options)
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