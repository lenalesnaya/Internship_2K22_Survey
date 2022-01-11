using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ItechArt.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ItechArt.Repositories;

public class UnitOfWork<TContext> : IUnitOfWork
    where TContext : DbContext
{
    private IDictionary<Type, object> _repositories;
    private bool _disposed;


    public TContext DbContext { get; }


    public UnitOfWork(TContext dbContext)
    {
        DbContext = dbContext
            ?? throw new ArgumentNullException(nameof(dbContext));
    }


    public IRepository<TEntity> GetRepository<TEntity>()
        where TEntity : class
    {
        if (_repositories == null)
        {
            _repositories = new Dictionary<Type, object>();
        }

        var type = typeof(TEntity);

        if (!_repositories.ContainsKey(type))
        {
            _repositories[type] = new Repository<TEntity>(DbContext);
        }

        return (IRepository<TEntity>)_repositories[type];
    }

    public async Task SaveChangesAsync()
    {
        await DbContext.SaveChangesAsync();
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
                
                DbContext.Dispose();
            }
        }

        _disposed = true;
    }
}