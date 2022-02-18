using System.Threading.Tasks;
using ItechArt.Common;
using ItechArt.Survey.DomainModel;

namespace ItechArt.Survey.Foundation.Authentication.Abstractions;

public interface IAuthenticateService
{ 
    Task<OperationResult<User, UserRegistrationStatusErrors>> RegistrationAsync(User user, string password);
}