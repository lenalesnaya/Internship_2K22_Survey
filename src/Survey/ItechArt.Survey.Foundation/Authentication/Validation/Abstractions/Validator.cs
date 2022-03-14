using ItechArt.Common.Validation;
using ItechArt.Common.Validation.Abstractions;
using ItechArt.Survey.DomainModel;
using ItechArt.Survey.Foundation.Authentication.Abstractions;

namespace ItechArt.Survey.Foundation.Authentication.Validation.Abstractions;

public abstract class Validator : IValidator<User, UserRegistrationErrors>
{
    public ValidationResult<UserRegistrationErrors> Validate(User user)
    {
        var error = ValidateWithErrorReturning(user);

        if (error.HasValue)
        {
            return ValidationResult<UserRegistrationErrors>.CreateFailureResult(error.Value);
        }

        return ValidationResult<UserRegistrationErrors>.CreateSuccessfulResult();
    }


    abstract protected UserRegistrationErrors? ValidateWithErrorReturning(User user);
}