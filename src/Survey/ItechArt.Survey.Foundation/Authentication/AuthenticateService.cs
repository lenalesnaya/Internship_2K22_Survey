using System.Threading.Tasks;
using ItechArt.Common;
using ItechArt.Survey.DomainModel;
using ItechArt.Survey.Foundation.Authentication.Abstractions;
using Microsoft.AspNetCore.Identity;


namespace ItechArt.Survey.Foundation.Authentication;

public class AuthenticateService : IAuthenticateService
{
    private readonly UserManager<User> _userManager;


    public AuthenticateService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }


    public async Task<OperationResult<User, UserRegistrationStatusErrors>> RegistrationAsync(User user, string password)
    {
        var userExists = await _userManager.FindByEmailAsync(user.Email);

        if (userExists != null)
        {
            return OperationResult<User, UserRegistrationStatusErrors>.FailureResult(
                UserRegistrationStatusErrors.UserAlreadyExists);
        }

        var createResult = await _userManager.CreateAsync(user, password);
        var addRoleResult = await _userManager.AddToRoleAsync(user, "User");

        return createResult.Succeeded
            ? OperationResult<User, UserRegistrationStatusErrors>.SuccessResult(user)
            : OperationResult<User, UserRegistrationStatusErrors>.FailureResult(UserRegistrationStatusErrors.UnknownError);
    }
}