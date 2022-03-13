using ItechArt.Common.Validation;
using ItechArt.Common.Validation.Abstractions;
using ItechArt.Survey.Foundation.Authentication.Abstractions;

namespace ItechArt.Survey.Foundation.Authentication.Validation.Abstractions;

public interface IPasswordValidator : IValidator<string, UserRegistrationErrors>
{
    new ValidationResult<UserRegistrationErrors> Validate(string password);
}