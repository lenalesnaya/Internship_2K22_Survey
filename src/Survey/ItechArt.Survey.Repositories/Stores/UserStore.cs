using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ItechArt.Repositories.Abstractions;
using ItechArt.Survey.DomainModel;
using Microsoft.AspNetCore.Identity;

namespace ItechArt.Survey.Repositories.Stores;

public class UserStore : IUserEmailStore<User>, IUserPasswordStore<User>
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

    public async Task<IdentityResult> CreateAsync(User user,  CancellationToken cancellationToken = default)
    {
        var repository = _unitOfWork.GetRepository<User>();
        repository.Add(user);
        
        try
        {
            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return IdentityResult.Failed(new IdentityError{ Description = $"Exception: {e}"});
        }

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> UpdateAsync(User user,  CancellationToken cancellationToken = default)
    {
        var repository = _unitOfWork.GetRepository<User>();
        repository.Update(user);

        try
        {
            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return IdentityResult.Failed(new IdentityError{ Description = $"Exception: {e}"});
        }

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken = default)
    {
        var repository = _unitOfWork.GetRepository<User>();
        repository.Remove(user);

        try
        {
            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return IdentityResult.Failed(new IdentityError{ Description = $"Exception: {e}"});
        }

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
            .SingleOrDefault();

    public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetEmailAsync(User user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<User> FindByEmailAsync(string email, CancellationToken cancellationToken)
        => (await _unitOfWork
                .GetRepository<User>()
                .GetWhereAsync(u =>
                    u.Email == email))
            .SingleOrDefault();

    public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
    {
        user.PasswordHash = passwordHash;
        await UpdateAsync(user, cancellationToken);
    }

    public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}