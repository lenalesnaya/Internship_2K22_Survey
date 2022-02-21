using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ItechArt.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ItechArt.Repositories;

public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet;


    public Repository(DbContext dbContext)
    {
        _dbSet = dbContext.Set<TEntity>();
    }


    public virtual async Task<IReadOnlyCollection<TEntity>> GetAllAsync(IEntityLoadStrategy<TEntity> loadStrategy = null)
    {
        return await IncludeEntities(loadStrategy).ToListAsync();
    }

    public virtual async Task<IReadOnlyCollection<TEntity>> GetWhereAsync(
        Expression<Func<TEntity, bool>> filter,
        IEntityLoadStrategy<TEntity> loadStrategy = null)
    {
        return await IncludeEntities(loadStrategy).Where(filter).ToListAsync();
    }

    public virtual async Task<TEntity> GetSingleAsync(
        Expression<Func<TEntity, bool>> filter,
        IEntityLoadStrategy<TEntity> loadStrategy = null)
    {
        return await IncludeEntities(loadStrategy).SingleAsync(filter);
    }

    public virtual async Task<TEntity> GetFirstOrDefaultAsync(IEntityLoadStrategy<TEntity> loadStrategy = null)
    {
        return await IncludeEntities(loadStrategy).FirstOrDefaultAsync();
    }

    public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter)
    {
        return await AnyAsync(filter);
    }

    public virtual void Add(TEntity entity)
    {
        _dbSet.Add(entity);
    }

    public virtual void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    public virtual void Remove(TEntity entity)
    {
        _dbSet.Remove(entity);
    }


    protected virtual IQueryable<TEntity> IncludeEntities(IEntityLoadStrategy<TEntity> loadStrategy = null)
    {
        return loadStrategy == null
            ? _dbSet
            : loadStrategy.Includes.Aggregate<Expression<Func<TEntity, object>>, IQueryable<TEntity>>(
                _dbSet,
                (q, include) => q.Include(include));
    }
}