using System.Threading.Tasks;
using ItechArt.Common;
using ItechArt.Survey.DomainModel;

namespace ItechArt.Survey.Foundation.Authentication.Abstractions;

public interface IAuthenticateService
{ 
    Task<OperationResult<UserRegistrationErrors>> RegistrationAsync(User user, string password);
}