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
            DbSet.Update(model);
        }
        else
        {
            DbSet.Add(model);
        }

        SurveyDbContext.SaveChanges();
    }

    public virtual async Task SaveAsync(TEntity model)
    {
        if (model.Id > 0)
        {
            DbSet.Update(model);
        }
        else
        {
            await DbSet.AddAsync(model);
        }

        await SurveyDbContext.SaveChangesAsync();
    }

    public void SaveChanges()
    {
        SurveyDbContext.SaveChanges();
    }

    public async Task SaveChangesAsync()
    { 
        await SurveyDbContext.SaveChangesAsync();
    }

    public virtual void Remove(TEntity model)
    {
        SurveyDbContext.Remove(model);
        SurveyDbContext.SaveChanges();
    }

    public virtual async Task RemoveAsync(TEntity model)
    {
        SurveyDbContext.Remove(model);
        await SurveyDbContext.SaveChangesAsync();
    }

    public virtual void Remove(int id)
    {
        var model = DbSet.SingleOrDefault(x => x.Id == id);

        Remove(model);
    }

    public virtual async Task RemoveAsync(int id)
    {
        var model = await DbSet.SingleOrDefaultAsync(x => x.Id == id);

        await RemoveAsync(model);
    }
}