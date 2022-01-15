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

    private readonly IDictionary<Type, Type> _typesOfRepositories;
    private readonly IDictionary<Type, object> _repositories;
    private bool _disposed;


    public UnitOfWork(TContext dbContext)
    {
        _dbContext = dbContext;

        _typesOfRepositories = new Dictionary<Type, Type>();
        _repositories = new Dictionary<Type, object>();
    }


    public IRepository<TEntity> GetRepository<TEntity>()
        where TEntity : class
    {
        var entityType = typeof(TEntity);

        if (_repositories.TryGetValue(entityType, out var repository))
        {
            return (IRepository<TEntity>)repository;
        }

        AddRepositoryToTheDictionary<TEntity>();

        return (IRepository<TEntity>)_repositories[entityType];
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

    protected void AddMapping<TEntity, TRepository>()
        where TEntity : class
        where TRepository : class, IRepository<TEntity>
    {
        _typesOfRepositories.Add(typeof(TEntity), typeof(TRepository));
    }


    private void AddRepositoryToTheDictionary<TEntity>()
        where TEntity : class
    {
        object repository = CreateRepository<TEntity>();

        _repositories.Add(typeof(TEntity), repository);
    }

    private object CreateRepository<TEntity>()
        where TEntity : class
    {
        object repository;

        if (_typesOfRepositories.TryGetValue(typeof(TEntity), out var typeOfRepository))
        {
            repository = Activator.CreateInstance(typeOfRepository, _dbContext);
        }
        else
        {
            repository = new Repository<TEntity>(_dbContext);
        }

        return repository;
    }
}