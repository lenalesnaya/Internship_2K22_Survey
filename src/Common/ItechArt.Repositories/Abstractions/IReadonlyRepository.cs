using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ItechArt.Repositories.Abstractions;

public interface IReadonlyRepository<TEntity>
    where TEntity : class
{
    Task<IReadOnlyCollection<TEntity>> GetAllAsync(IEntityLoadStrategy<TEntity> loadStrategy = null);

    Task<IReadOnlyCollection<TEntity>> GetWhereAsync(
        Expression<Func<TEntity, bool>> filter,
        IEntityLoadStrategy<TEntity> loadStrategy = null);

    Task<TEntity> GetSingleOrDefaultAsync(
        Expression<Func<TEntity, bool>> filter,
        IEntityLoadStrategy<TEntity> loadStrategy = null);

    Task<TEntity> GetFirstOrDefaultAsync(IEntityLoadStrategy<TEntity> loadStrategy = null);

    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter);
}