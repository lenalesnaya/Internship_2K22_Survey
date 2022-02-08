using System.Net;
using System.Threading.Tasks;
using ItechArt.Survey.DomainModel;
using ItechArt.Survey.Foundation.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace ItechArt.Survey.Foundation;

public class AuthenticateService : IAuthenticateService
{
    private readonly UserManager<User> _userManager;

    public AuthenticateService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<HttpStatusCode> RegistrationAsync(User entity, string password)
    {
        var userExists = await _userManager.FindByEmailAsync(entity.Email);

        if (userExists != null)
        {
            return HttpStatusCode.BadRequest;
        }

        var result = await _userManager.CreateAsync(entity, password);

        return result.Succeeded ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
    }
}