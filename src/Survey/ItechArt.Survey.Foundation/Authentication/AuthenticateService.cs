using System.Threading.Tasks;
using ItechArt.Common;
using ItechArt.Survey.DomainModel;
using ItechArt.Survey.Foundation.Authentication.Abstractions;
using ItechArt.Survey.Foundation.Authentication.Validation.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace ItechArt.Survey.Foundation.Authentication;

public class AuthenticateService : IAuthenticateService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IUserValidator _userValidator;


    public AuthenticateService(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IUserValidator userValidator)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _userValidator = userValidator;
    }


    public async Task<OperationResult<User, UserRegistrationErrors>> RegisterAsync(User user, string password)
    {
        var validationResult = _userValidator.Validate(user, password);
        if (!validationResult.IsSuccessful)
        {
            return OperationResult<User, UserRegistrationErrors>
                .CreateUnsuccessful(validationResult.Error.GetValueOrDefault());
        }

        var usernameExists = await _userManager.FindByNameAsync(user.UserName);
        if (usernameExists != null)
        {
            return OperationResult<User, UserRegistrationErrors>
                .CreateUnsuccessful(UserRegistrationErrors.UserNameAlreadyExists);
        }
        
        var userEmailExists = await _userManager.FindByEmailAsync(user.Email);
        if (userEmailExists != null)
        {
            return OperationResult<User, UserRegistrationErrors>
                .CreateUnsuccessful(UserRegistrationErrors.EmailAlreadyExists);
        }

        var createResult = await _userManager.CreateAsync(user, password);
        if (!createResult.Succeeded)
        {
            return OperationResult<User, UserRegistrationErrors>
                .CreateUnsuccessful(UserRegistrationErrors.UnknownError);
        }

        var roleResult = await _userManager.AddToRoleAsync(user, Role.User);
        if (!roleResult.Succeeded)
        {
            return OperationResult<User, UserRegistrationErrors>
                .CreateUnsuccessful(UserRegistrationErrors.UnknownError);
        }

        await _signInManager.SignInAsync(user, true);

        return OperationResult<User, UserRegistrationErrors>.CreateSuccessful(user);
    }

    public async Task SignOutAsync()
    {
        await _signInManager.SignOutAsync();
    }
}