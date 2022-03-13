using ItechArt.Common.Validation;
using ItechArt.Survey.DomainModel;
using ItechArt.Survey.Foundation.Authentication.Abstractions;
using ItechArt.Survey.Foundation.Authentication.Configuration;
using ItechArt.Survey.Foundation.Authentication.Validation.Abstractions;
using Microsoft.Extensions.Options;

namespace ItechArt.Survey.Foundation.Authentication.Validation;

public class UserValidator : IUserValidator
{
    private readonly RegistrationOptions _options;


    public UserValidator(IOptions<RegistrationOptions> options)
    {
        _options = options.Value;
    }


    public ValidationResult<UserRegistrationErrors> Validate(User user)
    {
        var validationResult = ValidateUserName(user.UserName);

        if (validationResult.HasError)
        {
            return ValidationResult<UserRegistrationErrors>.CreateResultWithError(validationResult.Error);
        }

        validationResult = ValidateEmail(user.Email);

        if (validationResult.HasError)
        {
            return ValidationResult<UserRegistrationErrors>.CreateResultWithError(validationResult.Error);
        }

        return ValidationResult<UserRegistrationErrors>.CreateResultWithoutError();
    }


    private ValidationResult<UserRegistrationErrors> ValidateUserName(string userName)
    {
        if (string.IsNullOrEmpty(userName))
        {
            return ValidationResult<UserRegistrationErrors>.CreateResultWithError(UserRegistrationErrors.UserNameIsRequired);
        }

        if (!(_options.UserNameMinLength <= userName.Length && userName.Length <= _options.UserNameMaxLength))
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
}