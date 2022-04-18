using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItechArt.Survey.DomainModel;
using ItechArt.Survey.Foundation.UserManagement.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace ItechArt.Survey.Foundation.UserManagement;

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

    public async Task<User> GetUserByNameAsync(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);

        return user;
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        return user;
    }

    public async Task<IList<User>> GetAllUsers()
    {
        var users = await _userManager.GetUsersInRoleAsync(Role.User);

        return users;
    }

    public async Task<IdentityResult> DeleteUser(int id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
        {
            throw new Exception();
        }

        return result;
    }
}