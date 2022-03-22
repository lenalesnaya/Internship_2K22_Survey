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

        var userWithGivenName = await _userManager.FindByNameAsync(user.UserName);
        if (userWithGivenName != null)
        {
            return OperationResult<User, UserRegistrationErrors>
                .CreateUnsuccessful(UserRegistrationErrors.UserNameAlreadyExists);
        }
        
        var userWithGivenEmail = await _userManager.FindByEmailAsync(user.Email);
        if (userWithGivenEmail != null)
        {
            return OperationResult<User, UserRegistrationErrors>
                .CreateUnsuccessful(UserRegistrationErrors.EmailAlreadyExists);
        }

        var creationResult = await _userManager.CreateAsync(user, password);
        if (!creationResult.Succeeded)
        {
            return OperationResult<User, UserRegistrationErrors>
                .CreateUnsuccessful(UserRegistrationErrors.UnknownError);
        }

        var addingToRoleResult = await _userManager.AddToRoleAsync(user, Role.User);
        if (!addingToRoleResult.Succeeded)
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