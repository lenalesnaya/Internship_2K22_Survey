using ItechArt.Common.Validation;
using ItechArt.Common.Validation.Abstractions;
using ItechArt.Survey.DomainModel.UserModel;
using ItechArt.Survey.Foundation.Authentication.Abstractions;

namespace ItechArt.Survey.Foundation.UserManagement.Validation.Abstractions;

public interface IUserValidator : IValidator<User, UserRegistrationErrors>
{
    ValidationResult<UserRegistrationErrors> Validate(User user, string password);
}