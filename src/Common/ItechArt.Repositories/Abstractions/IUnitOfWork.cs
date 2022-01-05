using System;
using System.Threading.Tasks;
using ItechArt.Survey.DomainModel.Interfaces;

namespace ItechArt.Repositories.Abstractions;

public interface IUnitOfWork : IDisposable
{
    IBaseReadonlyRepository<TEntity> GetReadonlyRepository<TEntity>() 
            where TEntity : class, IEntity;

    IBaseReadWriteRepository<TEntity> GetReadWriteRepository<TEntity>()
        where TEntity : class, IEntity;

    int SaveChanges();

    Task<int> SaveChangesAsync();
}