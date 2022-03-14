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
        var validationResult = _userValidator.Validate(user);

        if (validationResult.HasError)
        {
            return OperationResult<User, UserRegistrationErrors>.CreateFailureResult(validationResult.Error);
        }

        validationResult = _userValidator.Validate(password);

        if (validationResult.HasError)
        {
            return OperationResult<User, UserRegistrationErrors>.CreateFailureResult(validationResult.Error);
        }

        var userExists = await _userManager.FindByNameAsync(user.UserName);

        if (userExists != null)
        {
            return OperationResult<User, UserRegistrationErrors>.CreateFailureResult(UserRegistrationErrors.UserNameAlreadyExists);
        }

        userExists = await _userManager.FindByEmailAsync(user.Email);

        if (userExists != null)
        {
            return OperationResult<User, UserRegistrationErrors>.CreateFailureResult(UserRegistrationErrors.EmailAlreadyExists);
        }

        var createResult = await _userManager.CreateAsync(user, password);

        if (!createResult.Succeeded)
        {
            return OperationResult<User, UserRegistrationErrors>.CreateFailureResult(UserRegistrationErrors.UnknownError);
        }

        var roleResult = await _userManager.AddToRoleAsync(user, Role.User);

        if (!roleResult.Succeeded)
        {
            return OperationResult<User, UserRegistrationErrors>.CreateFailureResult(UserRegistrationErrors.UnknownError);
        }

        await _signInManager.SignInAsync(user, true);

        return OperationResult<User, UserRegistrationErrors>.CreateSuccessfulResult(user);
    }
}