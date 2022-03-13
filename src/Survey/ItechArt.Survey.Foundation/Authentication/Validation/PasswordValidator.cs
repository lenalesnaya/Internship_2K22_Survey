using ItechArt.Common.Validation;
using ItechArt.Survey.Foundation.Authentication.Abstractions;
using ItechArt.Survey.Foundation.Authentication.Configuration;
using ItechArt.Survey.Foundation.Authentication.Validation.Abstractions;
using Microsoft.Extensions.Options;

namespace ItechArt.Survey.Foundation.Authentication.Validation;

public class PasswordValidator : IPasswordValidator
{
    private readonly RegistrationOptions _options;


    public PasswordValidator(IOptions<RegistrationOptions> options)
    {
        _options = options.Value;
    }


    public ValidationResult<UserRegistrationErrors> Validate(string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            return ValidationResult<UserRegistrationErrors>.CreateResultWithError(UserRegistrationErrors.PasswordIsRequired);
        }

        if (!(_options.PasswordMinLength <= password.Length && password.Length <= _options.PasswordMaxLength))
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