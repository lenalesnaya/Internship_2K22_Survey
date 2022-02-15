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
            return OperationResult<User, UserRegistrationStatus>.FailureResult(
                UserRegistrationStatus.UserAlreadyExists,
                "User already exists");
        }
        
        var createResult = await _userManager.CreateAsync(user, password);
        var addRoleResult = await _userManager.AddToRoleAsync(user, "User");

        return createResult.Succeeded
            ? OperationResult<User, UserRegistrationStatus>.SuccessResult(user, UserRegistrationStatus.Ok)
            : OperationResult<User, UserRegistrationStatus>.FailureResult(UserRegistrationStatus.UnknownError, createResult.Errors.ToString());
    }

    // public async Task<OperationResult<User>> AuthenticateAsync(User user, string password)
    // {
    //     var userExists = await _userManager.FindByEmailAsync(user.Email);
    //     if (userExists != null && await _userManager.CheckPasswordAsync(user, password))
    //     {
    //         var claims = await _userManager.GetClaimsAsync(user);
    //         claims.Add(new Claim(user.Id.ToString()));
    //     }
    // }
}