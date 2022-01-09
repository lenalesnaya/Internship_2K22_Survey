using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ItechArt.Repositories.Abstractions;

public interface IReadonlyRepository<TEntity>
    where TEntity : class, IEntityId
{
    Task<IReadOnlyCollection<TEntity>> GetAllAsync();

    Task<IReadOnlyCollection<TEntity>> GetWhereAsync(Expression<Func<TEntity, bool>> filter);

    Task<IReadOnlyCollection<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes);

    Task<IReadOnlyCollection<TEntity>> GetWhereAsync(Expression<Func<TEntity, bool>> filter,
        params Expression<Func<TEntity, object>>[] includes);
}