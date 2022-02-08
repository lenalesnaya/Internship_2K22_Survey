using System.Net;
using System.Threading.Tasks;
using ItechArt.Survey.DomainModel;

namespace ItechArt.Survey.Foundation.Abstractions;

public interface IAuthenticateService
{ 
    Task<HttpStatusCode> RegistrationAsync(User entity, string password);
}