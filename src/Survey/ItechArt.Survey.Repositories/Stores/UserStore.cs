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


    public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(user.Id.ToString());
    }

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
            .SingleOrDefault();//пропарсить к int а потом 

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
        var rolesRepository = _unitOfWork.GetRepository<Role>();
        var userRolesRepository = _unitOfWork.GetRepository<UserRole>();

        var role = await rolesRepository.GetSingleWhereAsync(role => role.Name == roleName);

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

        var role = await rolesRepository.GetSingleWhereAsync(role => role.Name == roleName);

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

        var roleNames = new List<string>();
        var userRoles = await userRolesRepository.GetWhereAsync(userRole => userRole.UserId == user.Id);

        foreach (var userRole in userRoles)
        {
            roleNames.Add((await rolesRepository.GetSingleWhereAsync(role => role.Id == userRole.RoleId)).Name);// много для одной строки?
        }

        return roleNames;

        //var query = from userRole in userRoles
        //            where userRole.UserId.Equals(user.Id)
        //            join role in await repositoryOfRoles.GetAllAsync() on userRole.RoleId equals role.Id
        //            select role.Name;

        //return query.ToList();
    }

    public async Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken = default)
    {
        var rolesRepository = _unitOfWork.GetRepository<Role>();
        var userRolesRepository = _unitOfWork.GetRepository<UserRole>();

        var role = await rolesRepository.GetSingleWhereAsync(role => role.Name == roleName);
        var result = await userRolesRepository.AnyAsync(
            userRole => (userRole.UserId == user.Id) && (userRole.RoleId == role.Id));

        return result;
    }

    public async Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken = default)
    {
        var users = new List<User>();

        var rolesRepository = _unitOfWork.GetRepository<Role>();
        var userRolesRepository = _unitOfWork.GetRepository<UserRole>();
        var usersRepository = _unitOfWork.GetRepository<User>();

        var role = await rolesRepository.GetSingleWhereAsync(role => role.Name == roleName);
        var roleUsers = await userRolesRepository.GetWhereAsync(userRole => userRole.RoleId == role.Id);

        foreach (var roleUser in roleUsers)
        {
            users.Add(await usersRepository.GetSingleWhereAsync(user => user.Id == roleUser.UserId));
        }

        return users;
    }

    public void Dispose()
    {
        // is not needed
    }
}