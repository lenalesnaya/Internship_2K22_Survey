using ItechArt.Repositories.Abstractions;
using ItechArt.Survey.DomainModel;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace ItechArt.Survey.Foundation.Authentication.Stores;

public class RoleStore : IRoleStore<Role>
{
    private readonly IUnitOfWork _unitOfWork;


    public RoleStore(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken = default)
    {
        var repository = _unitOfWork.GetRepository<Role>();
        repository.Add(role);
        await _unitOfWork.SaveChangesAsync();

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken = default)
    {
        var repository = _unitOfWork.GetRepository<Role>();
        repository.Update(role);
        await _unitOfWork.SaveChangesAsync();

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken = default)
    {
        var repository = _unitOfWork.GetRepository<Role>();
        repository.Remove(role);
        await _unitOfWork.SaveChangesAsync();

        return IdentityResult.Success;
    }

    public Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(role.Id.ToString());
    }

    public Task<string> GetRoleNameAsync(Role role, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(role.Name);
    }

    public Task SetRoleNameAsync(Role role, string roleName, CancellationToken cancellationToken = default)
    {
        role.Name = roleName;

        return Task.CompletedTask;
    }

    public Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(role.NormalizedName);
    }

    public Task SetNormalizedRoleNameAsync(Role role, string normalizedName, CancellationToken cancellationToken = default)
    {
        role.NormalizedName = normalizedName;

        return Task.CompletedTask;
    }

    public async Task<Role> FindByIdAsync(string roleId, CancellationToken cancellationToken = default)
    {
        int intRoleId = int.Parse(roleId);
        var repository = _unitOfWork.GetRepository<Role>();
        var role = await repository.GetSingleOrDefaultAsync(role => role.Id == intRoleId);

        return role;
}

    public async Task<Role> FindByNameAsync(string roleName, CancellationToken cancellationToken = default)
    {
        var repository = _unitOfWork.GetRepository<Role>();
        var role = await repository.GetSingleOrDefaultAsync(role => role.Name == roleName);

        return role;
    }

    public void Dispose()
    {
        // is not needed
    }
}