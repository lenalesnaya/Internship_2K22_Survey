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


    public async Task<User> GetCurrent(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        return user;
    }
}