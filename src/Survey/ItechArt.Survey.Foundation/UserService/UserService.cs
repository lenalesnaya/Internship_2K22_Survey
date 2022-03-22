using System.Threading.Tasks;
using ItechArt.Survey.DomainModel;
using Microsoft.AspNetCore.Identity;

namespace ItechArt.Survey.Foundation.UserService;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;


    public UserService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }


    public async Task<User> GetUserByIdAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        return user;
    }

    public async Task<User> FindByUserNameAsync(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);

        return user;
    }

    public async Task<User> FindByEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        return user;
    }
}