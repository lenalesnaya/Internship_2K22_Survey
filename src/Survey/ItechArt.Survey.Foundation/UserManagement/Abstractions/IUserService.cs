using System.Collections.Generic;
using System.Threading.Tasks;
using ItechArt.Common;
using ItechArt.Survey.DomainModel.UserModel;
using Microsoft.AspNetCore.Identity;

namespace ItechArt.Survey.Foundation.UserManagement.Abstractions;

public interface IUserService
{
    Task<User> GetUserByIdAsync(string userId);

    Task<User> GetUserByNameAsync(string userName);

    Task<User> GetUserByEmailAsync(string email);

    Task<IList<User>> GetAllUsersAsync();

    Task<OperationResult<UserDeletionErrors>> DeleteUserAsync(int id);
}