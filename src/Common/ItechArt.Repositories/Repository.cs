using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ItechArt.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ItechArt.Repositories;

public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : class, IEntityId
{
    private readonly DbSet<TEntity> _dbSet;
    private readonly DbContext _dbContext;


    public Repository(DbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<TEntity>();
    }


    public virtual async Task<IReadOnlyCollection<TEntity>> GetAllAsync()
    {
        IQueryable<TEntity> query = _dbSet;

        return await query.ToListAsync();
    }

    public virtual async Task<IReadOnlyCollection<TEntity>> GetWhereAsync(
        Expression<Func<TEntity, bool>> filter)
    {
        IQueryable<TEntity> query = _dbSet;
        query = query.Where(filter);

        return await query.ToListAsync();
    }

    public virtual async Task<IReadOnlyCollection<TEntity>> GetAllAsync(
        params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = _dbSet;
        query = IncludeEntities(query, includes);

        return await query.ToListAsync();
    }

    public virtual async Task<IReadOnlyCollection<TEntity>> GetWhereAsync(
        Expression<Func<TEntity, bool>> filter,
        params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = _dbSet;
        query = IncludeEntities(query, includes);
        query = query.Where(filter);

        return await query.ToListAsync();
    }

    public virtual async Task<IReadOnlyCollection<TEntity>> GetAllNoTrackingAsync()
    {
        IQueryable<TEntity> query = _dbSet;

        return await query.AsNoTracking().ToListAsync();
    }

    public virtual async Task<IReadOnlyCollection<TEntity>> GetWhereNoTrackingAsync(
        Expression<Func<TEntity, bool>> filter)
    {
        IQueryable<TEntity> query = _dbSet;
        query = query.Where(filter);

        return await query.AsNoTracking().ToListAsync();
    }

    public virtual async Task<IReadOnlyCollection<TEntity>> GetAllNoTrackingAsync(
        params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = _dbSet;
        query = IncludeEntities(query, includes);

        return await query.AsNoTracking().ToListAsync();
    }

    public virtual async Task<IReadOnlyCollection<TEntity>> GetWhereNoTrackingAsync(
        Expression<Func<TEntity, bool>> filter,
        params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = _dbSet;
        query = IncludeEntities(query, includes);
        query = query.Where(filter);

        return await query.AsNoTracking().ToListAsync();
    }

    public virtual async Task AddAsync(TEntity model)
    {
        await _dbSet.AddAsync(model);
    }

    public virtual async Task UpdateByIdAsync(int id)
    {
        var model = await _dbSet.SingleOrDefaultAsync(x => x.Id == id);

        Update(model);
    }

    public virtual void Update(TEntity model)
    {
        _dbSet.Update(model);
    }

    public virtual async Task RemoveByIdAsync(int id)
    {
        var model = await _dbSet.SingleOrDefaultAsync(x => x.Id == id);

        Remove(model);
    }

    public virtual void Remove(TEntity model)
    {
        _dbContext.Remove(model);
    }


    protected virtual IQueryable<TEntity> IncludeEntities(IQueryable<TEntity> query,
        params Expression<Func<TEntity, object>>[] includes)
    {
        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return query;
    }
}