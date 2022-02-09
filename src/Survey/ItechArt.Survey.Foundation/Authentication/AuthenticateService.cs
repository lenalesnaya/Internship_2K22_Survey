using System.Net;
using System.Threading.Tasks;
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

    public async Task<OperationResult<int, UserRegistrationStatus>> RegistrationAsync(User entity, string password)
    {
        var userExists = await _userManager.FindByEmailAsync(entity.Email);

        if (userExists != null)
        {
           
            return new OperationResult<int, UserRegistrationStatus>
            {
                OperationStatus = UserRegistrationStatus.UserAlreadyExists
            };
        }

        var result = await _userManager.CreateAsync(entity, password);

        return result.Succeeded
            ? new OperationResult<int, UserRegistrationStatus>
            {
                Data = entity.Id,
                OperationStatus = UserRegistrationStatus.Ok
            }
            : new OperationResult<int, UserRegistrationStatus>
            {
                OperationStatus = UserRegistrationStatus.UnprocessableEntity
            };
    }
}