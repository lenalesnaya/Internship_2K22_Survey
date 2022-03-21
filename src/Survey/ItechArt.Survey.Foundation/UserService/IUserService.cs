using System.Threading.Tasks;
using ItechArt.Survey.DomainModel;

namespace ItechArt.Survey.Foundation.UserService;

public interface IUserService
{
    Task<User> GetUserByIdAsync(string userId);

    Task<User> FindByUserNameAsync(string userName);

    Task<User> FindByEmailAsync(string email);
}