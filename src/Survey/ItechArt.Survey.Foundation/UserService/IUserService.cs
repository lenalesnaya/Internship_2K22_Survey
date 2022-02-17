using System.Threading.Tasks;
using ItechArt.Survey.DomainModel;

namespace ItechArt.Survey.Foundation.UserService;

public interface IUserService
{
    Task<User> GetCurrent(string userId);
}