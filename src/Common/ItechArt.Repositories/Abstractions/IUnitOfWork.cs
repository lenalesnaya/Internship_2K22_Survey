using System;
using System.Threading.Tasks;

namespace ItechArt.Repositories.Abstractions;

public interface IUnitOfWork : IDisposable
{
    IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class, IEntityId;

    Task<int> SaveChangesAsync();
}