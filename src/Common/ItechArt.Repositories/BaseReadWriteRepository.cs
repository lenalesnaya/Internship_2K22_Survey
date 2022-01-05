using System.Linq;
using System.Threading.Tasks;
using ItechArt.Repositories.Abstractions;
using ItechArt.Survey.DomainModel.Interfaces;
using ItechArt.Survey.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ItechArt.Repositories;

public class BaseReadWriteRepository<TEntity>
    : BaseReadonlyRepository<TEntity>,
        IBaseReadWriteRepository<TEntity>
    where TEntity : class, IEntity
{
    public BaseReadWriteRepository(SurveyDbContext surveyDbContext) 
        : base(surveyDbContext)
    {
    }

    public virtual void Save(TEntity model)
    {
        if (model.Id > 0)
        {
            _dbSet.Update(model);
        }
        else
        {
            _dbSet.Add(model);
        }

        _surveyDbContext.SaveChanges();
    }

    public virtual async Task SaveAsync(TEntity model)
    {
        if (model.Id > 0)
        {
            _dbSet.Update(model);
        }
        else
        {
            await _dbSet.AddAsync(model);
        }

        await _surveyDbContext.SaveChangesAsync();
    }

    public void SaveChanges()
    {
        _surveyDbContext.SaveChanges();
    }

    public async Task SaveChangesAsync()
    { 
        await _surveyDbContext.SaveChangesAsync();
    }

    public virtual void Remove(TEntity model)
    {
        _surveyDbContext.Remove(model);
        _surveyDbContext.SaveChanges();
    }

    public virtual async Task RemoveAsync(TEntity model)
    {
        _surveyDbContext.Remove(model);
        await _surveyDbContext.SaveChangesAsync();
    }

    public virtual void Remove(int id)
    {
        var model = _dbSet.SingleOrDefault(x => x.Id == id);

        Remove(model);
    }

    public virtual async Task RemoveAsync(int id)
    {
        var model = await _dbSet.SingleOrDefaultAsync(x => x.Id == id);

        await RemoveAsync(model);
    }
}