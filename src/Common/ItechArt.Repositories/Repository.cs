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


    public virtual async Task<IReadOnlyCollection<TEntity>> GetAllAsync(
        params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = _dbSet;

        return await IncludeEntities(query, includes).ToListAsync();
    }

    public virtual async Task<IReadOnlyCollection<TEntity>> GetWhereAsync(
        Expression<Func<TEntity, bool>> filter,
        params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = _dbSet;
        query = IncludeEntities(query, includes);
        query = ToFilter(query,filter);

        return await query.ToListAsync();
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

    protected virtual IQueryable<TEntity> ToFilter(IQueryable<TEntity> query,
        Expression<Func<TEntity, bool>> filter)
    {
        query = query.Where(filter);

        return query;
    }

    protected virtual IQueryable<TEntity> IncludeEntities(IQueryable<TEntity> query,
        params Expression<Func<TEntity, object>>[] includes)
    {
        if (includes.Length > 0)
        {
            query = includes.Aggregate(query,
                (q, include) => q.Include(include));
        }

        return query;
    }
}