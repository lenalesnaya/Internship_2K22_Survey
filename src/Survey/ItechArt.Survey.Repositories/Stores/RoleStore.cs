using ItechArt.Repositories.Abstractions;
using ItechArt.Survey.DomainModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace ItechArt.Survey.Repositories.Stores;

public class RoleStore: IRoleStore<Role>
{
    private readonly IUnitOfWork _unitOfWork;


    public RoleStore(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken = default)
    {
        if (role == null)
        {
            throw new ArgumentNullException(nameof(role));
        }

        var repository = _unitOfWork.GetRepository<Role>();
        repository.Add(role);

        return await SaveChangesAsync();
    }

    public async Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken = default)
    {
        if (role == null)
        {
            throw new ArgumentNullException(nameof(role));
        }

        var repository = _unitOfWork.GetRepository<Role>();
        repository.Update(role);

        return await SaveChangesAsync();
    }

    public async Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken = default)
    {
        if (role == null)
        {
            throw new ArgumentNullException(nameof(role));
        }

         var repository = _unitOfWork.GetRepository<Role>();
        repository.Remove(role);

        return await SaveChangesAsync();
    }

    public Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken = default)
    {
        if (role == null)
        {
            throw new ArgumentNullException(nameof(role));
        }

        return Task.FromResult(role.Id.ToString());
    }

    public Task<string> GetRoleNameAsync(Role role, CancellationToken cancellationToken = default)
    {
        if (role == null)
        {
            throw new ArgumentNullException(nameof(role));
        }

        return Task.FromResult(role.Name);
    }

    public Task SetRoleNameAsync(Role role, string roleName, CancellationToken cancellationToken = default)
    {
        if (role == null)
        {
            throw new ArgumentNullException(nameof(role));
        }

        role.Name = roleName ?? throw new ArgumentNullException(nameof(roleName));

        return Task.CompletedTask;// or await Update...
    }

    public Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken = default)
    {
        if (role == null)
        {
            throw new ArgumentNullException(nameof(role));
        }

        return Task.FromResult(role.NormalizedName);
    }

    public Task SetNormalizedRoleNameAsync(Role role, string normalizedName, CancellationToken cancellationToken = default)
    {
        if (role == null)
        {
            throw new ArgumentNullException(nameof(role));
        }

        role.Name = normalizedName ?? throw new ArgumentNullException(nameof(normalizedName));

        return Task.CompletedTask;// or await Update...
    }

    public Task<Role> FindByIdAsync(string roleId, CancellationToken cancellationToken = default)
    {
        if (int.TryParse(roleId, out int intRoleId))
        {
            var repository = _unitOfWork.GetRepository<Role>();
            var role = repository.GetWhereAsync(role => role.Id == intRoleId).Result.SingleOrDefault();

            return Task.FromResult(role);
        }
        else
        {
            throw new ArgumentException("Parameter value must be parsed to int", nameof(roleId));
        }
    }

    public Task<Role> FindByNameAsync(string roleName, CancellationToken cancellationToken = default)
    {
        if (roleName == null)
        {
            throw new ArgumentNullException(nameof(roleName));
        }

        var repository = _unitOfWork.GetRepository<Role>();
        var role = repository.GetWhereAsync(role => role.Name == roleName).Result.SingleOrDefault();

        return Task.FromResult(role);
    }

    public void Dispose()
    {
        _unitOfWork.Dispose();
        GC.SuppressFinalize(this);
    }


    protected async Task<IdentityResult> SaveChangesAsync()
    {
        try
        {
            await _unitOfWork.SaveChangesAsync();
        }
        catch(Exception exception)
        {
            return IdentityResult.Failed(new IdentityError { Description = $"Exception: {exception}" });
        }

        return IdentityResult.Success;
    }
}