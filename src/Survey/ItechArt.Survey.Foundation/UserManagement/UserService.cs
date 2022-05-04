using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItechArt.Common;
using ItechArt.Common.Logging.Abstractions;
using ItechArt.Common.Logging.Extensions;
using ItechArt.Survey.DomainModel.UserModel;
using ItechArt.Survey.Foundation.Authentication.Configuration;
using ItechArt.Survey.Foundation.UserManagement.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace ItechArt.Survey.Foundation.UserManagement;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly ILogger _logger;

    public UserService(UserManager<User> userManager, ILogger logger)
    {
        _userManager = userManager;
        _logger = logger;
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

    public async Task<IList<User>> GetAllUsersAsync()
    {
        var users = await _userManager.GetUsersInRoleAsync(Role.User);

        return users;
    }

    public async Task<OperationResult<UserProfileErrors>> SetAvatarAsync(string userId, string avatarFilePath)
    {
        if (avatarFilePath != null)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.AvatarFilePath = avatarFilePath;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                _logger.LogWarning($"Avatar path updating is failed: {result.Errors.FirstOrDefault()}");

                return OperationResult<UserProfileErrors>.CreateUnsuccessful(UserProfileErrors.AvatarSettingIsFailed);
            }
        }

        return OperationResult<UserProfileErrors>.CreateSuccessful();
    }

    public async Task<OperationResult<UserProfileErrors>> SetDefaultAvatarAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        user.AvatarFilePath = RegistrationOptions.DefaultAvatarFolderPath + RegistrationOptions.DefaultAvatarFileName;

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            _logger.LogWarning($"Avatar path updating is failed: {result.Errors.FirstOrDefault()}");

            return OperationResult<UserProfileErrors>.CreateUnsuccessful(UserProfileErrors.DefaultAvatarSettingIsFailed);
        }

        return OperationResult<UserProfileErrors>.CreateSuccessful();
    }

    public async Task<OperationResult<UserDeletionErrors>> DeleteUserAsync(int id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
        {
            _logger.LogWarning($"Deletion is failed: {result.Errors.FirstOrDefault()}");

            return OperationResult<UserDeletionErrors>.CreateUnsuccessful(UserDeletionErrors.DeletionIsFailed);
        }

        return OperationResult<UserDeletionErrors>.CreateSuccessful();
    }
}