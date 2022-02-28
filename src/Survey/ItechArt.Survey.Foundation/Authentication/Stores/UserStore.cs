using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ItechArt.Repositories;
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

        return Task.CompletedTask;
    }

    public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(user.NormalizedUserName);
    }

    public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken = default)
    {
        user.NormalizedUserName = normalizedName;

        return Task.CompletedTask;
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

        return await repository.GetSingleOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User> FindByNameAsync(string userName, CancellationToken cancellationToken = default)
    {
        var repository = _unitOfWork.GetRepository<User>();

        return await repository.GetSingleOrDefaultAsync(u => u.UserName == userName);
    }

    public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken = default)
    {
        user.Email = email;

        return Task.CompletedTask;
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

        return Task.CompletedTask;
    }

    public async Task<User> FindByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var repository = _unitOfWork.GetRepository<User>();

        return await repository.GetSingleOrDefaultAsync(u => u.Email == email);
    }

    public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(user.NormalizedEmail);
    }

    public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken = default)
    {
        user.NormalizedEmail = normalizedEmail;

        return Task.CompletedTask;
    }

    public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken = default)
    {
        user.PasswordHash = passwordHash;

        return Task.CompletedTask;
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
        var role = await rolesRepository.GetSingleOrDefaultAsync(role => role.Name == roleName);

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
        var role = await rolesRepository.GetSingleOrDefaultAsync(role => role.Name == roleName);
        var userRole = await userRolesRepository.GetSingleOrDefaultAsync(
            userRole => (userRole.RoleId == role.Id) && (userRole.UserId == user.Id));

        userRolesRepository.Remove(userRole);
    }

    public async Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken = default)
    {
        var userRolesRepository = _unitOfWork.GetRepository<UserRole>();
        var userRoles = await userRolesRepository.GetWhereAsync(
            userRole => userRole.UserId == user.Id,
            new EntityLoadStrategy<UserRole>(userRole => userRole.Role));
        var roleNames = userRoles.Select(userRole => userRole.Role.Name);

        return roleNames.ToList();
    }

    public async Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken = default)
    {
        var userRolesRepository = _unitOfWork.GetRepository<UserRole>();
        var userRoles = await userRolesRepository.GetWhereAsync(
            userRole => userRole.UserId == user.Id,
            new EntityLoadStrategy<UserRole>(userRole => userRole.Role));
        var roleNames = userRoles.Select(userRole => userRole.Role.NormalizedName);

        return roleNames.Any(name => name.Equals(roleName));
    }

    public async Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken = default)
    {
        var userRolesRepository = _unitOfWork.GetRepository<UserRole>();
        var userRoles = await userRolesRepository.GetWhereAsync(
            userRole => userRole.Role.NormalizedName.Equals(roleName),
            new EntityLoadStrategy<UserRole>(userRole => userRole.Role, userRole => userRole.User));
        var users = userRoles.Select(userRole => userRole.User).ToList();

        return users;
    }

    public void Dispose()
    {
        // is not needed
    }
}