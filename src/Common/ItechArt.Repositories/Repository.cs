using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ItechArt.Repositories.Abstractions;
using ItechArt.Survey.DomainModel.Abstractions;
using ItechArt.Survey.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ItechArt.Repositories;

public class Repository<TEntity>
    : IRepository<TEntity>
    where TEntity : BaseEntity
{
    private DbSet<TEntity> DbSet;
    private SurveyDbContext SurveyDbContext;


    public Repository(SurveyDbContext surveyDbContext)
    {
        SurveyDbContext = surveyDbContext;
        DbSet = surveyDbContext.Set<TEntity>();
    }


    public virtual async Task<IReadOnlyCollection<TEntity>> GetAllAsync()
    {
        IQueryable<TEntity> query = DbSet;

        return await query.ToListAsync();
    }

    public virtual async Task<IReadOnlyCollection<TEntity>> GetWhereAsync(
        Expression<Func<TEntity, bool>> filter = null)
    {
        IQueryable<TEntity> query = DbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return await query.ToListAsync();
    }

    public virtual async Task<IReadOnlyCollection<TEntity>> GetAllAsync(
        params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = DbSet;

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return await query.ToListAsync();
    }
    
    public virtual async Task<IReadOnlyCollection<TEntity>> GetWhereAsync(
        Expression<Func<TEntity, bool>> filter = null,
        params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = DbSet;

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return await query.ToListAsync();
    }

    public virtual async Task<IReadOnlyCollection<TEntity>> GetAllNoTrackingAsync()
    {
        IQueryable<TEntity> query = DbSet;

        return await query.AsNoTracking().ToListAsync();
    }

    public virtual async Task<IReadOnlyCollection<TEntity>> GetWhereNoTrackingAsync(
        Expression<Func<TEntity, bool>> filter = null)
    {
        IQueryable<TEntity> query = DbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return await query.AsNoTracking().ToListAsync();
    }

    
    public virtual async Task<IReadOnlyCollection<TEntity>> GetAllNoTrackingAsync(
        params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = DbSet;

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return await query.AsNoTracking().ToListAsync();
    }
    
    public virtual async Task<IReadOnlyCollection<TEntity>> GetWhereNoTrackingAsync(
        Expression<Func<TEntity, bool>> filter = null,
        params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = DbSet;

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return await query.AsNoTracking().ToListAsync();
    }
    
    public virtual async Task AddAsync(TEntity model)
    {
        await DbSet.AddAsync(model);

        await SurveyDbContext.SaveChangesAsync();
    }

    public virtual async Task Update(TEntity model)
    {
        DbSet.Update(model);

        await SurveyDbContext.SaveChangesAsync();
    }

    public virtual async Task RemoveAsync(TEntity model)
    {
        SurveyDbContext.Remove(model);
        await SurveyDbContext.SaveChangesAsync();
    }

    public virtual async Task RemoveAsync(int id)
    {
        var model = await DbSet.SingleOrDefaultAsync(x => x.Id == id);

        await RemoveAsync(model);
    }
}