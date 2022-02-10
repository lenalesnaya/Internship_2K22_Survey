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

    public async Task<OperationResult<User, UserRegistrationStatus>> RegistrationAsync(User user, string password)
    {
        var userExists = await _userManager.FindByEmailAsync(user.Email);

        if (userExists != null)
        {
            return new OperationResult<User, UserRegistrationStatus>(UserRegistrationStatus.UserAlreadyExists);
        }

        var result = await _userManager.CreateAsync(user, password);

        return result.Succeeded
            ? new OperationResult<User, UserRegistrationStatus>(user, UserRegistrationStatus.Ok)
            : new OperationResult<User, UserRegistrationStatus>(UserRegistrationStatus.UnknownError, result.Errors.ToString());
    }
}