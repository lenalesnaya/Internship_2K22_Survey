using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ItechArt.Repositories.Abstractions;
using ItechArt.Survey.DomainModel;
using Microsoft.AspNetCore.Identity;

namespace ItechArt.Survey.Repositories.Stores;

public class UserStore : IUserStore<User>
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

    public async Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        => (await _unitOfWork
            .GetRepository<User>()
            .GetWhereAsync(u => 
                u.Id == user.Id))
            .SingleOrDefault()
            .ToString();

    public async Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        => (await _unitOfWork
            .GetRepository<User>()
            .GetWhereAsync(u => 
                u.UserName == user.UserName))
            .SingleOrDefault()
            .UserName;

    public async Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
    {
        user = (await _unitOfWork
            .GetRepository<User>()
            .GetWhereAsync(u =>
                u.Id == user.Id))
            .SingleOrDefault();
        user.UserName = userName;
    }

    public async Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        => (await _unitOfWork
                .GetRepository<User>()
                .GetWhereAsync(u => 
                    u.NormalizedUserName == user.NormalizedUserName))
            .SingleOrDefault()
            .NormalizedUserName;

    public async Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
    {
        user = (await _unitOfWork
                .GetRepository<User>()
                .GetWhereAsync(u =>
                    u.Id == user.Id))
            .SingleOrDefault();
        user.NormalizedEmail = normalizedName;
    }

    public async Task<IdentityResult> CreateAsync(User user,  CancellationToken cancellationToken)
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

    public async Task<IdentityResult> UpdateAsync(User user,  CancellationToken cancellationToken)
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

    public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
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

    public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        => (await _unitOfWork
            .GetRepository<User>()
            .GetWhereAsync(u => 
                u.Id.ToString() == userId))
            .SingleOrDefault();

    public async Task<User> FindByNameAsync(string userName, CancellationToken cancellationToken)
        => (await _unitOfWork
            .GetRepository<User>()
            .GetWhereAsync(u =>
                u.UserName.ToString() == userName))
            .SingleOrDefault();
}