using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ItechArt.Repositories.Abstractions;
using ItechArt.Survey.DomainModel;
using Microsoft.AspNetCore.Identity;

namespace ItechArt.Survey.Foundation.Authentication.Stores;

public class UserStore : IUserEmailStore<User>, IUserPasswordStore<User>, IUserRoleStore<User>
{
    private readonly IUnitOfWork _unitOfWork;


    public UserStore(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(user.Id.ToString());
    }

    public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(user.UserName);
    }

    public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken = default)
    {
        user.UserName = userName;

        return Task.FromResult(user);
    }

    public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(user.NormalizedUserName);
    }

    public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken = default)
    {
        user.NormalizedEmail = normalizedName;

        return Task.FromResult(user);
    }

    public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken = default)
    {
        var repository = _unitOfWork.GetRepository<User>();
        repository.Add(user);
        await _unitOfWork.SaveChangesAsync();

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        var repository = _unitOfWork.GetRepository<User>();
        repository.Update(user);
        await _unitOfWork.SaveChangesAsync();

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken = default)
    {
        var repository = _unitOfWork.GetRepository<User>();
        repository.Remove(user);
        await _unitOfWork.SaveChangesAsync();

        return IdentityResult.Success;
    }

    public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        var id = int.Parse(userId);
        var repository = _unitOfWork.GetRepository<User>();

        return await repository.GetSingleAsync(u => u.Id == id);
    }

    public async Task<User> FindByNameAsync(string userName, CancellationToken cancellationToken = default)
    {
        var repository = _unitOfWork.GetRepository<User>();

        return await repository.GetSingleAsync(u => u.UserName == userName);
    }

    public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken = default)
    {
        user.Email = email;

        return Task.FromResult(user);
    }

    public Task<string> GetEmailAsync(User user, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(user.Email);
    }

    public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(user.EmailConfirmed);
    }

    public Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken = default)
    {
        user.EmailConfirmed = confirmed;

        return Task.FromResult(user);
    }

    public async Task<User> FindByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var repository = _unitOfWork.GetRepository<User>();

        return await repository.GetSingleAsync(u => u.Email == email);
    }

    public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(user.NormalizedEmail);
    }

    public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken = default)
    {
        user.NormalizedEmail = normalizedEmail;

        return Task.FromResult(user);
    }

    public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken = default)
    {
        user.PasswordHash = passwordHash;

        return Task.FromResult(user);
    }

    public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(user.PasswordHash);
    }

    public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(user.PasswordHash != null);
    }

    public async Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken = default)
    {
        var rolesRepository = _unitOfWork.GetRepository<Role>();
        var userRolesRepository = _unitOfWork.GetRepository<UserRole>();
        var role = await rolesRepository.GetSingleAsync(role => role.Name == roleName);

        var userRole = new UserRole()
        {
            RoleId = role.Id,
            UserId = user.Id
        };

        userRolesRepository.Add(userRole);
    }

    public async Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken = default)
    {
        var rolesRepository = _unitOfWork.GetRepository<Role>();
        var userRolesRepository = _unitOfWork.GetRepository<UserRole>();
        var role = await rolesRepository.GetSingleAsync(role => role.Name == roleName);

        var userRole = new UserRole()
        {
            RoleId = role.Id,
            UserId = user.Id
        };

        userRolesRepository.Remove(userRole);
    }

    public async Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken = default)
    {
        var userRolesRepository = _unitOfWork.GetRepository<UserRole>();
        var rolesRepository = _unitOfWork.GetRepository<Role>();
        var userRoles = await userRolesRepository.GetWhereAsync(userRole => userRole.UserId == user.Id);

        var roleNames = new List<string>();

        foreach (var userRole in userRoles)
        {
            roleNames.Add((await rolesRepository.GetSingleAsync(
                role => role.Id == userRole.RoleId)).Name);
        }

        return roleNames;
    }

    public async Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken = default)
    {
        var rolesRepository = _unitOfWork.GetRepository<Role>();
        var userRolesRepository = _unitOfWork.GetRepository<UserRole>();

        var role = await rolesRepository.GetSingleAsync(role => role.Name == roleName);
        var result = await userRolesRepository.AnyAsync(
            userRole => (userRole.UserId == user.Id) && (userRole.RoleId == role.Id));

        return result;
    }

    public async Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken = default)
    {
        var rolesRepository = _unitOfWork.GetRepository<Role>();
        var userRolesRepository = _unitOfWork.GetRepository<UserRole>();
        var usersRepository = _unitOfWork.GetRepository<User>();

        var role = await rolesRepository.GetSingleAsync(role => role.Name == roleName);
        var roleUsers = await userRolesRepository.GetWhereAsync(userRole => userRole.RoleId == role.Id);

        var users = new List<User>();

        foreach (var roleUser in roleUsers)
        {
            users.Add(await usersRepository.GetSingleAsync(user => user.Id == roleUser.UserId));
        }

        return users;
    }

    public void Dispose()
    {
        // is not needed
    }
}