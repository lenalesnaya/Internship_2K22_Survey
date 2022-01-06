using System;
using System.Threading.Tasks;
using ItechArt.Survey.DomainModel.Abstractions;

namespace ItechArt.Repositories.Abstractions;

public interface IUnitOfWork : IDisposable
{
    IRepository<TEntity> GetRepository<TEntity>() 
            where TEntity : BaseEntity;

    int SaveChanges();

    Task<int> SaveChangesAsync();
}