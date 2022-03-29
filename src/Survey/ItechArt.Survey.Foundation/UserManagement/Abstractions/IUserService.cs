using System.Threading.Tasks;
using ItechArt.Survey.DomainModel;

namespace ItechArt.Survey.Foundation.UserManagement.Abstractions;

public interface IUserService
{
    Task<User> GetUserByIdAsync(string userId);

    Task<User> GetUserByNameAsync(string userName);

    Task<User> GetUserByEmailAsync(string email);
}