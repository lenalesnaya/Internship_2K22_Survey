using ItechArt.Repositories.Abstractions;
using ItechArt.Survey.DomainModel;
using System;
using System.Threading.Tasks;

namespace ItechArt.Survey.Repositories.Stores
{
    public class RoleStore<TRole> : IDisposable//: IRoleStore<TRole>
        where TRole : Role
    {
        private readonly IUnitOfWork _unitOfWork;


        public RoleStore(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public void CreateAsync(TRole role) // ?? should return Task<OperationResult<TResult, TStatus>>
        {
            var repository = _unitOfWork.GetRepository<TRole>();
            repository.Add(role);

            _unitOfWork.SaveChangesAsync();
        }

        public void DeleteAsync(TRole role) // ?? should return Task<OperationResult<TResult, TStatus>>
        {
            var repository = _unitOfWork.GetRepository<TRole>();
            repository.Remove(role);

            _unitOfWork.SaveChangesAsync();
        }

        public Task<TRole> FindByIdAsync(int roleId)
        {
            var repository = _unitOfWork.GetRepository<TRole>();
            var collectionWithRole = repository.GetWhereAsync(role => role.Id == roleId).Result;
            TRole role = null;

            foreach (var item in collectionWithRole)
            {
                role = item;
            }

            return Task.FromResult(role);
        }

        public Task<TRole> FindByNameAsync(string roleName)
        {
            var repository = _unitOfWork.GetRepository<TRole>();
            var collectionWithRole = repository.GetWhereAsync(role => role.Name == roleName).Result;
            TRole role = null;

            foreach (var item in collectionWithRole)
            {
                role = item;
            }

            return Task.FromResult(role);
        }

        public void SetRoleNameAsync(TRole role, string roleName)
        {
            var repository = _unitOfWork.GetRepository<TRole>();
            role.Name = roleName;
            repository.Update(role);

            _unitOfWork.SaveChangesAsync();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
            GC.SuppressFinalize(this);
        }

        //public Task<string> GetNormalizedRoleNameAsync(TRole role, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<string> GetRoleIdAsync(TRole role)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<string> GetRoleNameAsync(TRole role)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task SetNormalizedRoleNameAsync(TRole role, string normalizedName, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IdentityResult> UpdateAsync(TRole role)
        //{
        //    throw new NotImplementedException();
        //}
    }
}