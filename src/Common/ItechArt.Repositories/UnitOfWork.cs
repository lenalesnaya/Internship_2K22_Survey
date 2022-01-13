using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ItechArt.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ItechArt.Repositories;

public class UnitOfWork<TContext> : IUnitOfWork
    where TContext : DbContext
{
    private readonly TContext _dbContext;

    private readonly IDictionary<Type, Type> _repositoryMappings;
    private readonly IDictionary<Type, object> _repositories;
    private bool _disposed;


    public UnitOfWork(TContext dbContext)
    {
        _dbContext = dbContext;

        _repositoryMappings = new Dictionary<Type, Type>();
        _repositories = new Dictionary<Type, object>();
    }


    public void AddMapping<TEntity, TRepository>()
        where TEntity : class
        where TRepository : class, IRepository<TEntity>
    {
        _repositoryMappings.Add(typeof(TEntity), typeof(TRepository));
    }

    public IRepository<TEntity> GetRepository<TEntity>()
        where TEntity : class
    {
        var entityType = typeof(TEntity);

        if (_repositories.TryGetValue(entityType, out var repository))
        {
            return (IRepository<TEntity>)repository;
        }

        if (_repositoryMappings.TryGetValue(entityType, out var repositoryType))
        {
            return GetRepositaryByMapping<TEntity>(entityType, repositoryType);
        }

        return new Repository<TEntity>(_dbContext);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }


    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _repositories?.Clear();

                _dbContext.Dispose();
            }
        }

        _disposed = true;
    }

    protected virtual IRepository<TEntity> GetRepositaryByMapping<TEntity>(Type entityType, Type repositoryType)
        where TEntity : class
    {
        var repository = Activator.CreateInstance(repositoryType, _dbContext);
        _repositories.Add(entityType, repository);

        return (IRepository<TEntity>)repository;
    }
}