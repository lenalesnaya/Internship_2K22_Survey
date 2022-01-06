using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ItechArt.Survey.DomainModel.Abstractions;

namespace ItechArt.Repositories.Abstractions;

public interface IReadonlyRepository<TEntity> 
    where TEntity : BaseEntity
{ 
    Task<IReadOnlyCollection<TEntity>> GetAllAsync();

    Task<IReadOnlyCollection<TEntity>> GetWhereAsync(Expression<Func<TEntity, bool>> filter = null);

    Task<IReadOnlyCollection<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes);

    Task<IReadOnlyCollection<TEntity>> GetWhereAsync(
        Expression<Func<TEntity, bool>> filter = null,
        params Expression<Func<TEntity, object>>[] includes);
}