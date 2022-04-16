using System.Threading.Tasks;
using ItechArt.Common;
using ItechArt.Survey.DomainModel.UserModel;

namespace ItechArt.Survey.Foundation.Authentication.Abstractions;

public interface IAuthenticateService
{
    Task<OperationResult<User, UserRegistrationErrors>> RegisterAsync(User user, string password);

    Task<OperationResult<UserAuthenticationErrors>> AuthenticateAsync(string userName, string password);

    Task SignOutAsync();
}