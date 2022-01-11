using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ItechArt.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ItechArt.Repositories;

public class UnitOfWork<TContext> : IUnitOfWork
    where TContext : DbContext
{
    private readonly TContext _dbContext;

    private readonly IDictionary<Type, object> _repositories;
    private bool _disposed;


    public UnitOfWork(TContext dbContext)
    {
        _dbContext = dbContext;

        _repositories = new Dictionary<Type, object>();
    }


    public IRepository<TEntity> GetRepository<TEntity>()
        where TEntity : class
    {
        var type = typeof(TEntity);

        _repositories.TryAdd(type, new Repository<TEntity>(_dbContext));

        return (IRepository<TEntity>)_repositories[type];
    }

    public TRepository GetCustomRepository<TEntity, TRepository>()
    where TEntity : class
    where TRepository : class, IRepository<TEntity>
    {
        var customRepository = _dbContext.GetService<TRepository>();
        if (customRepository != null)
        {
            return customRepository;
        }

        return null;
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        DisposeManagedResources();
        GC.SuppressFinalize(this);
    }


    protected virtual void DisposeManagedResources()
    {
        if (!_disposed)
        {
            _repositories?.Clear();

            _dbContext.Dispose();
        }

        _disposed = true;
    }
}