using System.Threading.Tasks;
using ItechArt.Common;
using ItechArt.Survey.DomainModel;
using ItechArt.Survey.Foundation.Authentication.Abstractions;
using ItechArt.Survey.Foundation.UserManagement.Validation.Abstractions;
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


    public async Task<OperationResult<UserRegistrationErrors>> RegisterAsync(User user, string password)
    {
        var validationResult = _userValidator.Validate(user, password);
        if (!validationResult.IsSuccessful)
        {
            return OperationResult<UserRegistrationErrors>
                .CreateUnsuccessful(validationResult.Error.GetValueOrDefault());
        }

        var userWithGivenName = await _userManager.FindByNameAsync(user.UserName);
        if (userWithGivenName != null)
        {
            return OperationResult<UserRegistrationErrors>
                .CreateUnsuccessful(UserRegistrationErrors.UserNameAlreadyExists);
        }
        
        var userWithGivenEmail = await _userManager.FindByEmailAsync(user.Email);
        if (userWithGivenEmail != null)
        {
            return OperationResult<UserRegistrationErrors>
                .CreateUnsuccessful(UserRegistrationErrors.EmailAlreadyExists);
        }

        var creationResult = await _userManager.CreateAsync(user, password);
        if (!creationResult.Succeeded)
        {
            return OperationResult<UserRegistrationErrors>
                .CreateUnsuccessful(UserRegistrationErrors.UnknownError);
        }

        var roleResult = await _userManager.AddToRoleAsync(user, Role.User);
        if (!roleResult.Succeeded)
        {
            return OperationResult<UserRegistrationErrors>
                .CreateUnsuccessful(UserRegistrationErrors.UnknownError);
        }

        await _signInManager.SignInAsync(user, true);

        return OperationResult<User, UserRegistrationErrors>.CreateSuccessful(user);
    }

    public async Task<OperationResult<UserAuthenticationErrors>> AuthenticateAsync(string userName, string password)
    {
        var signInResult = await _signInManager.PasswordSignInAsync(userName, password, true, false);
        if (!signInResult.Succeeded)
        {
            return OperationResult<UserAuthenticationErrors>
                .CreateUnsuccessful(UserAuthenticationErrors.InvalidEmailOrPassword);
        }

        return OperationResult<string, UserAuthenticationErrors>.CreateSuccessful();
    }

    public async Task SignOutAsync()
    {
        await _signInManager.SignOutAsync();
    }
}