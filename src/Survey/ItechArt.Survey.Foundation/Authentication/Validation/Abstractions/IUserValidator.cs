using ItechArt.Common.Validation;
using ItechArt.Common.Validation.Abstractions;
using ItechArt.Survey.DomainModel;
using ItechArt.Survey.Foundation.Authentication.Abstractions;

namespace ItechArt.Survey.Foundation.Authentication.Validation.Abstractions;

public interface IUserValidator : IValidator<User, UserRegistrationErrors>
{
    ValidationResult<UserRegistrationErrors> Validate(string password);
}