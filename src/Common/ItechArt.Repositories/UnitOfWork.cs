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

        if(_repositories.TryGetValue(type, out object value))
        {
            return (IRepository<TEntity>)value;
        }

        var repositoryType = typeof(IRepository<TEntity>);
        var repositoryInstance = Activator.CreateInstance(
            repositoryType.MakeGenericType(typeof(TEntity)), _dbContext);
        _repositories.Add(type, repositoryInstance);

        return (IRepository<TEntity>)_repositories[type];
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
}