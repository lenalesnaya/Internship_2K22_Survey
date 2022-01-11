using System;
using System.Threading.Tasks;

namespace ItechArt.Repositories.Abstractions;

public interface IUnitOfWork : IDisposable
{
    IRepository<TEntity> GetRepository<TEntity>()
        where TEntity : class;

    TRepository GetCustomRepository<TEntity, TRepository>()
        where TEntity : class
        where TRepository : class, IRepository<TEntity>;

    Task SaveChangesAsync();
}