using System.Threading.Tasks;
using ItechArt.Common;
using ItechArt.Survey.DomainModel;
using ItechArt.Survey.Foundation.Authentication.Abstractions;
using ItechArt.Survey.Foundation.Authentication.Validation;
using Microsoft.AspNetCore.Identity;

namespace ItechArt.Survey.Foundation.Authentication;

public class AuthenticateService : IAuthenticateService
{
    private readonly UserManager<User> _userManager;


    public AuthenticateService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }


    public async Task<OperationResult<User, UserRegistrationErrors>> RegisterAsync(User user, string password)
    {
        var userValidatorResult = UserValidator.Validate(user, password);

        if (!userValidatorResult.Success)
        {
            return userValidatorResult;
        }

        var userExists = await _userManager.FindByEmailAsync(user.Email);

        if (userExists != null)
        {
            return OperationResult<User, UserRegistrationErrors>.CreateFailureResult(UserRegistrationErrors.UserAlreadyExists);
        }

        var createResult = await _userManager.CreateAsync(user, password);
        await _userManager.AddToRoleAsync(user, "User");

        return createResult.Succeeded
            ? OperationResult<User, UserRegistrationErrors>.CreateSuccessfulResult(user)
            : OperationResult<User, UserRegistrationErrors>.CreateFailureResult(UserRegistrationErrors.UnknownError);
    }
}