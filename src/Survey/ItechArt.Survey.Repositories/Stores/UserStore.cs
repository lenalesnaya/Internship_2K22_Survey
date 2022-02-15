using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ItechArt.Repositories.Abstractions;
using ItechArt.Survey.DomainModel;
using Microsoft.AspNetCore.Identity;

namespace ItechArt.Survey.Repositories.Stores;

public class UserStore : IUserEmailStore<User>, IUserPasswordStore<User>, IUserRoleStore<User>
{
    private readonly IUnitOfWork _unitOfWork;

    public UserStore(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void Dispose()
    {
        _unitOfWork.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken = default)
        => (await _unitOfWork
            .GetRepository<User>()
            .GetWhereAsync(u =>
                u.Id == user.Id))
            .SingleOrDefault()
            .ToString();

    public async Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken = default)
        => (await _unitOfWork
            .GetRepository<User>()
            .GetWhereAsync(u =>
                u.UserName == user.UserName))
            .SingleOrDefault()
            .UserName;

    public async Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken = default)
    {
        user.UserName = userName;
        await UpdateAsync(user, cancellationToken);
    }

    public async Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken = default)
        => (await _unitOfWork
                .GetRepository<User>()
                .GetWhereAsync(u =>
                    u.NormalizedUserName == user.NormalizedUserName))
            .SingleOrDefault()
            .NormalizedUserName;

    public async Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken = default)
    {
        user.NormalizedEmail = normalizedName;
        await UpdateAsync(user, cancellationToken);
    }

    public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken = default)
    {
        var repository = _unitOfWork.GetRepository<User>();
        repository.Add(user);

        return await SaveChangesAsync();
    }

    public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        var repository = _unitOfWork.GetRepository<User>();
        repository.Update(user);

        return await SaveChangesAsync();
    }

    public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken = default)
    {
        var repository = _unitOfWork.GetRepository<User>();
        repository.Remove(user);

        return await SaveChangesAsync();
    }

    public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken = default)
        => (await _unitOfWork
            .GetRepository<User>()
            .GetWhereAsync(u =>
                u.Id.ToString() == userId))
            .SingleOrDefault();

    public async Task<User> FindByNameAsync(string userName, CancellationToken cancellationToken = default)
        => (await _unitOfWork
            .GetRepository<User>()
            .GetWhereAsync(u =>
                u.UserName.ToString() == userName))
            .SingleOrDefault();

    public async Task SetEmailAsync(User user, string email, CancellationToken cancellationToken)
    {
        user.Email = email;
        await UpdateAsync(user, cancellationToken);
    }

    public async Task<string> GetEmailAsync(User user, CancellationToken cancellationToken)
        => (await _unitOfWork
            .GetRepository<User>()
            .GetWhereAsync(u => 
                u.Email == user.Email))
            .SingleOrDefault()
            .Email;

    public async Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken)
        => (await _unitOfWork
                .GetRepository<User>()
                .GetWhereAsync(u => 
                    u.EmailConfirmed == user.EmailConfirmed))
            .SingleOrDefault()
            .EmailConfirmed;

    public async Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
    {
        user.EmailConfirmed = confirmed;
        await UpdateAsync(user, cancellationToken);
    }

    public async Task<User> FindByEmailAsync(string email, CancellationToken cancellationToken)
        => (await _unitOfWork
                .GetRepository<User>()
                .GetWhereAsync(u =>
                    u.Email == email))
            .SingleOrDefault();

    public async Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken)
        => (await _unitOfWork
                .GetRepository<User>()
                .GetWhereAsync(u => 
                    u.NormalizedEmail == user.NormalizedEmail))
            .SingleOrDefault()
            .NormalizedEmail;

    public async Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken)
    {
        user.NormalizedEmail = normalizedEmail;
        await UpdateAsync(user, cancellationToken);
    }

    public async Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
    {
        user.PasswordHash = passwordHash;
        await UpdateAsync(user, cancellationToken);
    }

    public async Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        => (await _unitOfWork
                .GetRepository<User>()
                .GetWhereAsync(u => 
                    u.PasswordHash == user.PasswordHash))
            .SingleOrDefault()
            .PasswordHash;

    public async Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
    {
        user = (await _unitOfWork
            .GetRepository<User>()
            .GetWhereAsync(u => u.Id == user.Id)).SingleOrDefault();
        return user.PasswordHash != null;
    }

    public async Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken = default)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        var repositoryOfRoles = _unitOfWork.GetRepository<Role>();
        var repositoryOfUserRoles = _unitOfWork.GetRepository<UserRole>();

        var roleId = repositoryOfRoles.GetWhereAsync(role => role.Name == roleName).Result.SingleOrDefault().Id;

        UserRole userRole = new()
        {
            RoleId = roleId,
            UserId = user.Id
        };

        repositoryOfUserRoles.Add(userRole);

        await SaveChangesAsync();
    }

    public async Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken = default)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        var repositoryOfRoles = _unitOfWork.GetRepository<Role>();
        var repositoryOfUserRoles = _unitOfWork.GetRepository<UserRole>();

        var roleId = repositoryOfRoles.GetWhereAsync(role => role.Name == roleName).Result.SingleOrDefault().Id;

        UserRole userRole = new()
        {
            RoleId = roleId,
            UserId = user.Id
        };

        repositoryOfUserRoles.Remove(userRole);

        await SaveChangesAsync();
    }

    public Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken = default)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        IList<string> roleNames = new List<string>();

        var repositoryOfUserRoles = _unitOfWork.GetRepository<UserRole>();
        var repositoryOfRoles = _unitOfWork.GetRepository<Role>();

        var userRoles = repositoryOfUserRoles.GetWhereAsync(userRole => userRole.UserId == user.Id).Result;

        foreach(var userRole in userRoles)
        {
            roleNames.Add(repositoryOfRoles.GetWhereAsync(role => role.Id == userRole.RoleId).Result.SingleOrDefault().Name);
        }

        return Task.FromResult(roleNames);
    }

    public Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken = default)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        if (roleName == null)
        {
            throw new ArgumentNullException(nameof(roleName));
        }

        var repositoryOfRoles = _unitOfWork.GetRepository<Role>();
        var repositoryOfUserRoles = _unitOfWork.GetRepository<UserRole>();

        var role = repositoryOfRoles.GetWhereAsync(role => role.Name == roleName).Result.SingleOrDefault();
        var result = repositoryOfUserRoles.GetAllAsync().Result.Any(
            userRole => (userRole.UserId == user.Id) && (userRole.RoleId == role.Id));

        return Task.FromResult(result);
    }

    public Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken = default)
    {
        if (roleName == null)
        {
            throw new ArgumentNullException(nameof(roleName));
        }

        IList<User> users = new List<User>();

        var repositoryOfRoles = _unitOfWork.GetRepository<Role>();
        var repositoryOfUserRoles = _unitOfWork.GetRepository<UserRole>();
        var repositoryOfUsers = _unitOfWork.GetRepository<User>();

        var role = repositoryOfRoles.GetWhereAsync(role => role.Name == roleName).Result.SingleOrDefault();
        var roleUsers = repositoryOfUserRoles.GetWhereAsync(userRole => userRole.RoleId == role.Id).Result;

        foreach (var roleUser in roleUsers)
        {
            users.Add(repositoryOfUsers.GetWhereAsync(user => user.Id == roleUser.UserId).Result.SingleOrDefault());
        }

        return Task.FromResult(users);
    }


    protected async Task<IdentityResult> SaveChangesAsync()
    {
        try
        {
            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception exception)
        {
            return IdentityResult.Failed(new IdentityError { Description = $"Exception: {exception}" });
        }

        return IdentityResult.Success;
    }
}