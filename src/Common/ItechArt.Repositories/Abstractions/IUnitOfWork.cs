using System;
using System.Threading.Tasks;

namespace ItechArt.Repositories.Abstractions;

public interface IUnitOfWork : IDisposable
{
    void AddMapping<TEntity, TRepository>()
        where TEntity : class
        where TRepository : class, IRepository<TEntity>;

    IRepository<TEntity> GetRepository<TEntity>()
        where TEntity : class;

    Task SaveChangesAsync();
}