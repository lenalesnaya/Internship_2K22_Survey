using ItechArt.Repositories.Abstractions;
using ItechArt.Survey.DomainModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace ItechArt.Survey.Repositories.Stores
{
    public class RoleStore<TRole> : IRoleStore<TRole>
        where TRole : Role
    {
        private readonly IUnitOfWork _unitOfWork;


        public RoleStore(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<IdentityResult> CreateAsync(TRole role, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            var repository = _unitOfWork.GetRepository<TRole>();
            repository.Add(role);

            return await SaveChangesAsync();
        }

        public async Task<IdentityResult> UpdateAsync(TRole role, CancellationToken cancellationToken = default)
        {
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            var repository = _unitOfWork.GetRepository<TRole>();
            repository.Update(role);

            return await SaveChangesAsync();
        }

        public async Task<IdentityResult> DeleteAsync(TRole role, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            var repository = _unitOfWork.GetRepository<TRole>();
            repository.Remove(role);

            return await SaveChangesAsync();
        }

        public Task<string> GetRoleIdAsync(TRole role, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            return Task.FromResult(role.Id.ToString());
        }

        public Task<string> GetRoleNameAsync(TRole role, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            return Task.FromResult(role.Name);
        }

        public Task SetRoleNameAsync(TRole role, string roleName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            role.Name = roleName ?? throw new ArgumentNullException(nameof(roleName));

            return Task.CompletedTask;
        }

        public Task<string> GetNormalizedRoleNameAsync(TRole role, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            return Task.FromResult(role.NormalizedName);
        }

        public Task SetNormalizedRoleNameAsync(TRole role, string normalizedName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            role.Name = normalizedName ?? throw new ArgumentNullException(nameof(normalizedName));

            return Task.CompletedTask;
        }

        public Task<TRole> FindByIdAsync(string roleId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (int.TryParse(roleId, out int intRoleId))
            {
                var repository = _unitOfWork.GetRepository<TRole>();
                var collectionWithRole = repository.GetWhereAsync(role => role.Id == intRoleId).Result;

                TRole role = collectionWithRole.FirstOrDefault();

                return Task.FromResult(role);
            }
            else
            {
                throw new ArgumentException("Parameter value must be parsed to int", nameof(roleId));
            }
        }

        public Task<TRole> FindByNameAsync(string roleName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (roleName == null)
            {
                throw new ArgumentNullException(nameof(roleName));
            }

            var repository = _unitOfWork.GetRepository<TRole>();
            var collectionWithRole = repository.GetWhereAsync(role => role.Name == roleName).Result;

            TRole role = collectionWithRole.FirstOrDefault();

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
            catch
            {
                return IdentityResult.Failed();
            }

            return IdentityResult.Success;
        }
    }
}